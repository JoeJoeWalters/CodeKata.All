using FluentAssertions;
using System;
using System.Collections.Generic;
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
                values[position.X][position.Y] = string.Empty; // blank out the string so we don't attempt it again (could be done with a lookup but quicker for now)

                // Find the next item depending on the current location
                if (
                    position.Y < values.Length
                    && position.X + 1 < values[position.Y].Length
                    && values[position.Y][position.X + 1] != string.Empty)
                {
                    position = new Point(position.X + 1, position.Y);
                }
                else
                    position = new Point(-1, -1);

                taken = (position.X >= 0 && position.Y >= 0) ?
                        values[position.Y][position.X] :
                        string.Empty;
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

        public static string[] ExpectedResult1 = new string[] { "1", "2", "3", "6", "9", "8", "7", "4", "5" };

        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { SnailTest1, ExpectedResult1 }
            };

            return allData.Take(numTests);
        }

        [Theory]
        [MemberData(nameof(GetData), 1)]
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
