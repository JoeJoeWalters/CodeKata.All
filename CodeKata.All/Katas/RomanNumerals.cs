using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Katas.RomanNumerals
{
    /*
    Problem:

    The Romans were a clever bunch. They conquered most of Europe and ruled it for hundreds of years. They invented concrete and straight roads and even bikinis [1]. One thing they never discovered though was the number zero. This made writing and dating extensive histories of their exploits slightly more challenging, but the system of numbers they came up with is still in use today. For example the BBC uses Roman numerals to date their programmes.

    The Romans wrote numbers using letters : I, V, X, L, C, D, M. (notice these letters have lots of straight lines and are hence easy to hack into stone tablets)

    Part I
    The Kata says you should write a function to convert from normal numbers to Roman Numerals: eg

         1 --> I
         10 --> X
         7 --> VII
    etc.

    For a full description of how it works, take a look at [this useful reference website] : which includes an implementation of the Kata in javascript.

    There is no need to be able to convert numbers larger than about 3000. (The Romans themselves didn’t tend to go any higher)

    Part II
    Write a function to convert in the other direction, ie numeral to digit

    Clues
    Can you make the code really beautiful and highly readable?
    does it help to break out lots of small named functions from the main function, or is it better to keep it all in one function?
    if you don’t know an algorithm to do this already, can you derive one using strict TDD?
    does the order you take the tests in affect the final design of your algorithm?
    Would it be better to work out an algorithm first before starting with TDD?
    if you do know an algorithm already, can you implement it using strict TDD?
    Can you think of another algorithm?
    what are the best data structures for storing all the numeral letters? (I, V, D, M etc)
    can you define the test cases in a csv file and use FIT, or generate test cases in xUnit?
    what is the best way to verify your tests are correct?
    */

    #region Solution
    public class Solution
    {
        private Dictionary<char, int> numerals = new Dictionary<char, int>()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        public Solution()
        {
        }

        public string FromDigit(int digit)
        {
            decimal decimalDigit = (decimal)digit; // Hack to allo division without compiler warning

            // How many of each major grouping?
            int thousands = (int)(Math.Floor(decimalDigit / 1000) % 10);
            int hundreds = (int)(Math.Floor(decimalDigit / 100) % 10);
            int tens = (int)(Math.Floor(decimalDigit / 10) % 10);
            int ones = digit % 10;

            return $"{new String('M', thousands)}{NumeralCast(hundreds, 'C', 'D', 'M')}{NumeralCast(tens, 'X', 'L', 'C')}{NumeralCast(ones, 'I', 'V', 'X')}";
        }

        private String NumeralCast(int amount, char small, char mid, char large)
        {
            switch (amount)
            {
                case 1:
                case 2:
                case 3:
                    return new String(small, amount);
                case 4:
                    return $"{small}{mid}";
                case 5:
                    return $"{mid}";
                case 6:
                case 7:
                case 8:
                    return $"{mid}{new String(small, amount - 5)}";
                case 9:
                    return $"{small}{large}";
            }

            return String.Empty;
        }

        public int FromNumerals(string numerals)
        {
            int rollingValue = 0;
            int lastValue = 0;
            for (int position = 0; position < numerals.Length; position++)
            {
                int value = GetDigit(numerals[position]);
                rollingValue += value;
                lastValue = value;
            }

            return rollingValue;
        }

        private int GetDigit(char numeral)
        {
            if (numerals.ContainsKey(numeral))
                return numerals[numeral];
            else
                return 0;
        }

    }
    #endregion

    #region Tests
    public class Tests
    {
        private readonly Solution converter;

        public Tests()
        {
            converter = new Solution();
        }

        [Theory]
        [InlineData(1, "I")]
        [InlineData(2, "II")]
        [InlineData(3, "III")]
#warning TODO: Finish reading multiple items correctly
        //[InlineData(4, "IV")]
        [InlineData(5, "V")]
        [InlineData(10, "X")]
        [InlineData(50, "L")]
        [InlineData(100, "C")]
        [InlineData(500, "D")]
        [InlineData(1000, "M")]
        public void Numeral_To_Digit_Equals_ExpectedValue(int digit, string numeral)
        {
            // ARRANGE

            // ACT
            int fromNumeral = converter.FromNumerals(numeral);

            // ASSERT
            fromNumeral.Should().Be(digit);
        }

        [Theory]
        [InlineData(1, "I")]
        [InlineData(2, "II")]
        [InlineData(3, "III")]
        [InlineData(4, "IV")]
        [InlineData(5, "V")]
        [InlineData(10, "X")]
        [InlineData(50, "L")]
        [InlineData(100, "C")]
        [InlineData(500, "D")]
        [InlineData(1000, "M")]
        [InlineData(1500, "MD")]
        [InlineData(1501, "MDI")]
        [InlineData(1504, "MDIV")]
        [InlineData(1984, "MCMLXXXIV")]
        [InlineData(2021, "MMXXI")]
        public void Digit_To_Numeral_Equals_ExpectedValue(int digit, string numeral)
        {
            // ARRANGE

            // ACT
            string fromDigit = converter.FromDigit(digit);

            // ASSERT
            fromDigit.Should().Be(numeral);
        }
    }
    #endregion
}
