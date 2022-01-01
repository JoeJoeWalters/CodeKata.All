using FluentAssertions;
using System.Collections.Generic;
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
            return new string[] { };
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
