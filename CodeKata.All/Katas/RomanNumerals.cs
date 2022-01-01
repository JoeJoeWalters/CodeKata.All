using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Katas.RomanNumerals
{
    /*
    Problem:
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
