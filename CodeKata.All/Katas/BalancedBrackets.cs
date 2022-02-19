using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Katas.BalancedBrackets
{
    /*
    Problem:
    */

    public interface IBracketChecker
    {
        Boolean Test(string testString);
    }

    #region Solution

    public class NPlus1Solution : IBracketChecker
    {
        public Boolean Test(string testString)
        {
            String beforeString;
            String afterString = testString;

            do
            {
                beforeString = afterString;
                
                afterString = afterString.Replace("{}", String.Empty);
                afterString = afterString.Replace("[]", String.Empty);
                afterString = afterString.Replace("()", String.Empty);

            } while (beforeString != afterString);

            return afterString == String.Empty;
        }
    }

    public class NSolution : IBracketChecker
    {

        private Dictionary<char, char> pairs = new Dictionary<char, char>()
        {
            {'{', '}' },
            {'(', ')' },
            {'[', ']' }
        };

        public NSolution()
        {
        }

        public Boolean Test(string testString)
        {
            Stack<char> expectedCloses = new Stack<char>();

            foreach (char character in testString)
            {
                if (pairs.ContainsKey(character))
                {
                    expectedCloses.Push(pairs[character]);
                }
                else
                {
                    char expectedCharacter = expectedCloses.Pop();
                    if (character != expectedCharacter) return false;
                }
            }

            return expectedCloses.Count == 0;
        }
    }
    #endregion

    #region Tests
    public class Tests
    {
        private IBracketChecker bracketChecker;

        public Tests()
        {
            bracketChecker = new NSolution();
        }

        [Theory]
        [InlineData("{}")]
        [InlineData("()")]
        [InlineData("[]")]
        public void When_Single_Pairs_Do_They_Balance(string testString)
        {
            // ARRANGE

            // ACT
            var result = bracketChecker.Test(testString);

            // ASSERT
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("{]")]
        [InlineData("[)")]
        [InlineData("[}")]
        public void When_No_Single_Pairs_Do_They_Not_Balance(string testString)
        {
            // ARRANGE

            // ACT
            var result = bracketChecker.Test(testString);

            // ASSERT
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("{()}")]
        [InlineData("([])")]
        [InlineData("[{}]")]
        public void When_Multiple_Pairs_Do_They_Balance(string testString)
        {
            // ARRANGE

            // ACT
            var result = bracketChecker.Test(testString);

            // ASSERT
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("{(})")]
        [InlineData("([)]")]
        [InlineData("[{]}")]
        public void When_Multiple_Pairs_Out_Of_Line_Do_They_Not_Balance(string testString)
        {
            // ARRANGE

            // ACT
            var result = bracketChecker.Test(testString);

            // ASSERT
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("{()[]{}}")]
        [InlineData("([{}][][])")]
        [InlineData("[{}()()]")]
        public void When_Multiple_Complex_Pairs_Do_They_Balance(string testString)
        {
            // ARRANGE

            // ACT
            var result = bracketChecker.Test(testString);

            // ASSERT
            result.Should().BeTrue();
        }
    }
    #endregion
}
