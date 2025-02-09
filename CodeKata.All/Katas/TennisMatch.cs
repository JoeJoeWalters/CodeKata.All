using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Katas.TennisMatch
{
    /*
    Problem:

    This Kata is about implementing a simple tennis game. I came up with it while thinking about Wii tennis, where they have simplified tennis, so each set is one game.

    The scoring system is rather simple:

    Each player can have either of these points in one game “love” “15” “30” “40”
    If you have 40 and you win the point you win the game, however there are special rules.
    If both have 40 the players are “deuce”.
    If the game is in deuce, the winner of a point will have advantage
    If the player with advantage wins the ball he wins the game
    If the player without advantage wins they are back at deuce.
    Alternate description of the rules per Wikipedia (http://en.wikipedia.org/wiki/Tennis#Scoring ):

    A game is won by the first player to have won at least four points in total and at least two points more than the opponent.
    The running score of each game is described in a manner peculiar to tennis: scores from zero to three points are described as “love”, “15”, “30”, and “40” respectively.
    If at least three points have been scored by each player, and the scores are equal, the score is “deuce”.
    If at least three points have been scored by each side and a player has one more point than his opponent, the score of the game is “advantage” for the player in the lead.

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
