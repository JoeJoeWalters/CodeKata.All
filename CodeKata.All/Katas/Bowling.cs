using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Katas.Bowling
{
    /*
    Problem:
    */

    #region Solution
    public class BowlingGame
    {
        public Player[] Players = new Player[2];

        public BowlingGame()
        {
        }

        public BowlingGame(string player1Symbols, string player2Symbols)
        {
            Players[0] = new Player(player1Symbols);
            Players[1] = new Player(player2Symbols);
        }
    }

    public class Player
    {
        public List<Frame> Frames = new List<Frame>();

        public int TotalScore
        {
            get
            {
                List<KeyValuePair<int, int>> pins = new List<KeyValuePair<int, int>>();

                int position = 0;
                for (int frameId = 0; frameId < Frames.Count; frameId++)
                {
                    Frame Frame = Frames[frameId];
                    position = frameId + 1;

                    if (Frame.Scores[0] >= 10)
                    {
                        pins.Add(new KeyValuePair<int, int>(position, Frame.Scores[0]));
                    }
                    else
                    {
                        pins.AddRange(Frame.Scores.Select(x => new KeyValuePair<int, int>(position, x)));
                    }
                }

                int result = 0;
                position = 0;
                while (position < pins.Count)
                {
                    int frameId = pins[position].Key;
                    int pinScore = pins[position].Value;

                    Boolean singleRoll = pins.Where(x => x.Key == frameId).Count() == 1;
                    Boolean strike = (pinScore == 10) && singleRoll;
                    //Boolean spare = !singleRoll && pins.Where(x => x.Key == frameId).Sum(x => x.Value) == 10;

                    // Last game?
                    if (frameId == 10)
                    {
                        // Add the sum of all remaining throws (should be only up to 2 more)
                        result += pins.Where(x => x.Key >= 10).Sum(x => x.Value);

                        // Must be finished
                        break;
                    }
                    else if (frameId < 10)
                    {
                        result += pinScore;

                        // Strike (one score of 10 in the frame)? Not a -/ so add next two balls
                        if (strike)
                        {
                            if (position < pins.Count - 1)
                                result += pins[position + 1].Value;

                            if (position < pins.Count - 2)
                                result += pins[position + 2].Value;
                        }
                        /*else if (spare)
                        {
                        }*/
                    }

                    position++;
                }

                return result;
            }
        }

        public Player(string symbols)
        {
            string[] split = symbols.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string symbol in split)
            {
                Frames.Add(new Frame(symbol));
            }
        }
    }

    public class Frame
    {
        // Pins knocked down in each (try)
        public List<int> Scores = new List<int>();

        public Frame()
        {

        }

        public Frame(string symbol)
        {
            symbol = symbol.ToLower();
            if (symbol == "x")
            {
                Scores.Add(10);
            }
            else if (symbol.EndsWith("-"))
            {
                int.TryParse(symbol.Replace("-", ""), out int try1Check);
                Scores.Add(try1Check);
                Scores.Add(0);
            }
            else if (symbol.StartsWith("-"))
            {
                Scores.Add(0);
                string remainder = symbol.Replace("-", "");
                if (remainder == "/")
                {
                    Scores.Add(10);
                }
                else
                {
                    int.TryParse(remainder, out int try2Check);
                    Scores.Add(try2Check);
                }
            }
            else if (symbol.Contains("/"))
            {
                string[] parts = symbol.Split('/', StringSplitOptions.RemoveEmptyEntries);
                Scores.Add((parts.Length != 0) ? int.Parse(parts[0].ToString()) : 0);
                Scores.Add(10 - Scores[0]);
                if (parts.Length == 2)
                    Scores.Add(int.Parse(parts[1]));
            }
        }
    }
    #endregion

    #region Tests
    public class Tests
    {

        [Theory]
        [InlineData("X", 10, 0)]
        [InlineData("-10", 0, 10)]
        [InlineData("9-", 9, 0)]
        [InlineData("--", 0, 0)]
        [InlineData("0/", 0, 10)]
        [InlineData("5/", 5, 5)]
        [InlineData("2/", 2, 8)]
        [InlineData("-/", 0, 10)]
        public void Symbol_To_Score_Checks(string symbol, int try1, int try2)
        {
            // ARRANGE
            var frame = new Frame(symbol);

            // ACT

            // ASSERT
            frame.Scores[0].Should().Be(try1);
            if (frame.Scores.Count > 1)
                frame.Scores[1].Should().Be(try2);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("X X X X X X X X X X X X", 300)] // Maximum score, (12 rolls, 12 strikes = 10 frames * 30 points = 300)
        [InlineData("-- X X X X X X X X X X X", 270)] // No score on first frame, strikes on all remaining including 2x bonus throws at end
        [InlineData("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-", 90)] // (20 rolls: 10 pairs of 9 and miss) = 10 frames * 9 points = 90
        [InlineData("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5", 150)] // (21 rolls: 10 pairs of 5 and spare, with a final 5) = 10 frames * 15 points = 150
        [InlineData("-/ 5/ -- -- -- -- -- -- -- --", 20)] // No pins then spare in first frame, 5 pins then spare in next frame then no score on remaining = 20
        [InlineData("X -/ 5/ -- -- -- -- -- -- --", 40)] // Strike, so 10 + next 2 balls, next two balls are 0 and 10 so 20 + 0 + 10 + 5 + 5 = 40
        [InlineData("-- 1/ 2/ 3/ 4/ 5/ 6/ 7/ 8/ 9/5", 40)] // Nothing then incrememntal spares with a 5 bonus throw 
        public void ScoreText_To_Score(string symbols, int expectedScore)
        {
            // ARRANGE
            BowlingGame game = new BowlingGame(symbols, "");

            // ACT

            // ASSERT
            game.Players[0].TotalScore.Should().Be(expectedScore);

        }
    }
    #endregion
}
