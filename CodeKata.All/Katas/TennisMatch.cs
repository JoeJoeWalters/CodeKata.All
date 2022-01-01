using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Katas.TennisMatch
{
    /*
    Problem:
    */

    #region Solution
    public class Solution
    {
        public Dictionary<int, string> scoreNames = new Dictionary<int, string>()
            {
                {0, "love" },
                {1, "fifteen" },
                {2, "thirty" },
                {3, "fourty" }
            };

        public int Player1Score { get; set; } = 0;
        public string Player1ScoreText { get => scoreNames[Player1Score]; }

        public int Player2Score { get; set; } = 0;
        public string Player2ScoreText { get => scoreNames[Player2Score]; }

        public string Score { get => CalculateGameScore(); }

        public Solution()
        {
        }

        private string CalculateGameScore()
        {
            if ((Player1Score == Player2Score) && Player1Score >= 3)
            {
                return "deuce";
            }
            else if (Player1Score >= 4 && (Player1Score - Player2Score == 1))
            {
                return "Player1 Advantage";
            }
            else if (Player2Score >= 4 && (Player2Score - Player1Score == 1))
            {
                return "Player2 Advantage";
            }
            else if (Player1Score >= 4 && (Player1Score - Player2Score >= 2))
            {
                return "Player1 Wins";
            }
            else if (Player2Score >= 4 && (Player2Score - Player1Score >= 2))
            {
                return "Player2 Wins";
            }
            else
            {
                return $"{Player1ScoreText} - {Player2ScoreText}";
            }
        }
    }
    #endregion

    #region Tests
    public class Tests
    {
        [Fact]
        public void Player1_and_Player2_should_start_with_0_points()
        {
            var game = new Solution();
            game.Player1ScoreText.Should().Be("love");
            game.Player2ScoreText.Should().Be("love");
        }

        [Theory]
        [InlineData(0, "love")]
        [InlineData(1, "fifteen")]
        [InlineData(2, "thirty")]
        [InlineData(3, "fourty")]
        public void Player1_Score_Test_Equals_Expected_Score(int score, string result)
        {
            var game = new Solution();
            game.Player1Score = score;
            game.Player1ScoreText.Should().Be(result);
        }

        [Fact]
        public void Players_are_at_deuce_when_both_players_have_40_points()
        {
            var game = new Solution();
            game.Player1Score = 3;
            game.Player2Score = 3;
            game.Score.Should().Be("deuce");
        }

        [Fact]
        public void Player1_is_at_advantage_when_player1_scores_a_point_after_deuce()
        {
            var game = new Solution();
            game.Player1Score = 4;
            game.Player2Score = 3;
            game.Score.Should().Be("Player1 Advantage");
        }

        [Fact]
        public void Player2_is_at_advantage_when_player2_scores_a_point_after_deuce()
        {
            var game = new Solution();
            game.Player1Score = 3;
            game.Player2Score = 4;
            game.Score.Should().Be("Player2 Advantage");
        }

        [Fact]
        public void Player1_wins_when_player1_scores_at_least_four_points_and_is_2_points_more_than_player2()
        {
            var game = new Solution();
            game.Player1Score = 6;
            game.Player2Score = 4;
            game.Score.Should().Be("Player1 Wins");
        }

        [Fact]
        public void Player2_wins_when_player2_scores_at_least_four_points_and_is_2_points_more_than_player1()
        {
            var game = new Solution();
            game.Player1Score = 4;
            game.Player2Score = 6;
            game.Score.Should().Be("Player2 Wins");
        }
    }
    #endregion
}
