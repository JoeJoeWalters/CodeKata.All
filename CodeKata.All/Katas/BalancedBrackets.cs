using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Katas.BalancedBrackets
{
    /*
    Problem:

    Given a string s representing an expression containing various types of brackets: {}, (), and [], the task is to determine whether the brackets in the expression are balanced or not. A balanced expression is one where every opening bracket has a corresponding closing bracket in the correct order.

    Example: 

    Input: s = “[{()}]”
    Output: true
    Explanation:  All the brackets are well-formed.


    Input: s = “[()()]{}”
    Output: true
    Explanation: All the brackets are well-formed.


    Input: s = “([]”
    Output: false
    Explanation: The expression is not balanced as there is a missing ‘)’ at the end.


    Input:  s = “([{]})”
    Output: false
    Explanation: The expression is not balanced because there is a closing ‘]’ before the closing ‘}’.
    */

    #region Solution
    public class Solution
    {

        private Dictionary<char, char> pairs = new Dictionary<char, char>()
        {
            {'{', '}' },
            {'(', ')' },
            {'[', ']' }
        };

        public Solution()
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

            return true;
        }
    }
    #endregion

    #region Tests
    public class Tests
    {
        [Theory]
        [InlineData("{}")]
        [InlineData("()")]
        [InlineData("[]")]
        public void When_Single_Pairs_Do_They_Balance(string testString)
        {
            // ARRANGE
            var bracketChecker = new Solution();

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
            var bracketChecker = new Solution();

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
            var bracketChecker = new Solution();

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
            var bracketChecker = new Solution();

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
            var bracketChecker = new Solution();

            // ACT
            var result = bracketChecker.Test(testString);

            // ASSERT
            result.Should().BeTrue();
        }
    }
    #endregion
}
