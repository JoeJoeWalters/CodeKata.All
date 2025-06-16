using AwesomeAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Katas.SnailSort
{
    /*
    Problem:

    SnailSort is an algorithm that takes an array of equal-length sub-arrays, and then merges them into a single array in a clockwise spiral, starting from the upper-left hand corner.

    For example, it turns

    [
        [1, 2, 3],
        [4, 5, 6],
        [7, 8, 9]
    ]

    to
        [1, 2, 3, 6, 9, 8, 7, 4, 5]

    */

    #region Solution
    public class SnailSorter
    {
        public SnailSorter()
        {

        }

        public string[] Sort(string[][] values)
        {
            // No data or first line is empty then throw
            if (values == null ||
                values.Length == 0 || 
                values[0].Length == 0)
                throw new ArrayTypeMismatchException();

            Point position = new Point(0, 0);
            string taken = values[position.Y][position.X];
            IEnumerable<string> sorted = new string[] { };

            while (taken != string.Empty)
            {
                sorted = sorted.Append(taken);
                Debug.WriteLine($"Clearing x:{position.X}, y:{position.Y}");
                values[position.Y][position.X] = string.Empty; // blank out the string so we don't attempt it again (could be done with a lookup but quicker for now)

                // Try and move right it there is something to grab
                if (
                    position.Y < values.Length - 1
                    && position.X < values[position.Y].Length - 1
                    && values[position.Y][position.X + 1] != string.Empty
                    )
                {
                    position = new Point(position.X + 1, position.Y);
                }
                // otherwise try and move down
                else if (
                    position.Y < values.Length - 1
                    && values[position.Y + 1][position.X] != string.Empty
                    )
                {
                    position = new Point(position.X, position.Y + 1);
                }
                // otherwise try and move left
                else if (
                    position.X > 0
                    && values[position.Y][position.X - 1] != string.Empty
                    )
                {
                    position = new Point(position.X - 1, position.Y);
                }
                // otherwise move up
                else if (
                    position.Y > 0
                    && values[position.Y - 1][position.X] != string.Empty
                    )
                {
                    position = new Point(position.X, position.Y - 1);
                }
                else
                    position = new Point(-1, -1); // Must be finished

                // Somethign to grab? Then grab it otherwise set the loop exit clause
                taken = (position.X >= 0 && position.Y >= 0) ?
                        values[position.Y][position.X] :
                        string.Empty;

                Debug.WriteLine($"Taking x:{position.X}, y:{position.Y} = {taken}");
            }

            return sorted.ToArray();
        }
    }
    #endregion

    #region Tests
    public class Tests
    {
        public static string[][] SnailTest1x1 =
            new string[][] {
                new string[] { "1" }
            };

        public static string[][] SnailTest3x3 =
            new string[][] {
                new string[] { "1", "2", "3" },
                new string[] { "4", "5", "6" },
                new string[] { "7", "8", "9" }
            };

        public static string[][] SnailTest4x4 =
            new string[][] {
                new string[] { "1", "2", "3", "4" },
                new string[] { "5", "6", "7", "8" },
                new string[] { "9", "10", "11", "12" },
                new string[] { "13", "14", "15", "16" }
            };

        public static string[][] SnailTest3x4 =
            new string[][] {
                new string[] { "1", "2", "3" },
                new string[] { "5", "6", "7" },
                new string[] { "9", "10", "11" },
                new string[] { "13", "14", "15" }
            };

        public static string[] ExpectedResult1x1 = new string[] { "1" };
        public static string[] ExpectedResult3x3 = new string[] { "1", "2", "3", "6", "9", "8", "7", "4", "5" };
        public static string[] ExpectedResult4x4 = new string[] { "1", "2", "3", "4", "8", "12", "16", "15", "14", "13", "9", "5", "6", "7", "11", "10" };
        public static string[] ExpectedResult3x4 = new string[] { "1", "2", "3", "7", "11", "15", "14", "13", "9", "5", "6", "10" };
        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { SnailTest1x1, ExpectedResult1x1 },
                new object[] { SnailTest3x3, ExpectedResult3x3 },
                new object[] { SnailTest4x4, ExpectedResult4x4 },
                new object[] { SnailTest3x4, ExpectedResult3x4 }
            };

            return allData;
        }

        public static IEnumerable<object[]> GetFailureData()
        {
            var allData = new List<object[]>
            {
                new object[] { null },
                new object[] { new string[][] { new string[] { } } },
                new object[] { new string[][] { } }
            };

            return allData;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void With_TwoDimensionalArray_Produce_Expected_OneDimensionalArray(string[][] toSort, string[] sorted)
        {
            // ARRANGE
            SnailSorter sorter = new SnailSorter();

            // ACT
            string[] result = sorter.Sort(toSort);

            // ASSERT
            result.Should().BeEquivalentTo(sorted);
        }

        [Theory]
        [MemberData(nameof(GetFailureData))]
        public void When_EmptyArray_Throw_AppropriateException(string[][] toSort)
        {
            // ARRANGE
            SnailSorter sorter = new SnailSorter();

            // ACT
            Action act = () => sorter.Sort(toSort);

            // ASSERT
            act.Should().Throw<ArrayTypeMismatchException>();
        }
    }
    #endregion
}
