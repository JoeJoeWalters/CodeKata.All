using FluentAssertions;
using Xunit;

namespace Katas.Template
{
    /*
    Problem:
    */

    #region Solution
    public class Solution
    {

    }
    #endregion

    #region Tests
    public class Tests
    {
        [Fact]
        public void Something_Does_Something_With_Thing_Expecting_SomethingElse()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        [Theory]
        [InlineData("Scenario 1")]
        [InlineData("Scenario 2")]
        [InlineData("Scenario 3")]
        [InlineData("Scenario 4")]
        public void Something_Does_SomethingSpecific_With_Thing_Expecting_SomethingElse(string scenario)
        {
            // ARRANGE

            // ACT

            // ASSERT
        }
    }
    #endregion
}