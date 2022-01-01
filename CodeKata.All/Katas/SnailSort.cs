using FluentAssertions;
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
        public static string[][] SnailTest1 =
            new string[][] {
                new string[] { "1", "2", "3" },
                new string[] { "4", "5", "6" },
                new string[] { "7", "8", "9" }
            };

        public static string[][] SnailTest2 =
            new string[][] {
                new string[] { "1", "2", "3", "4" },
                new string[] { "5", "6", "7", "8" },
                new string[] { "9", "10", "11", "12" },
                new string[] { "13", "14", "15", "16" }
            };

        public static string[][] SnailTest3 =
            new string[][] {
                new string[] { "1", "2", "3" },
                new string[] { "5", "6", "7" },
                new string[] { "9", "10", "11" },
                new string[] { "13", "14", "15" }
            };

        public static string[] ExpectedResult1 = new string[] { "1", "2", "3", "6", "9", "8", "7", "4", "5" };
        public static string[] ExpectedResult2 = new string[] { "1", "2", "3", "4", "8", "12", "16", "15", "14", "13", "9", "5", "6", "7", "11", "10" };
        public static string[] ExpectedResult3 = new string[] { "1","2","3","7","11","15","14","13","9","5","6","10" };
        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { SnailTest1, ExpectedResult1 },
                new object[] { SnailTest2, ExpectedResult2 },
                new object[] { SnailTest3, ExpectedResult3 }
            };

            return allData.Take(numTests);
        }

        [Theory]
        [MemberData(nameof(GetData), 3)]
        public void Something_Does_SomethingSpecific_With_Thing_Expecting_SomethingElse(string[][] toSort, string[] sorted)
        {
            // ARRANGE
            SnailSorter sorter = new SnailSorter();

            // ACT
            string[] result = sorter.Sort(toSort);

            // ASSERT
            result.Should().BeEquivalentTo(sorted);
        }
    }
    #endregion
}
