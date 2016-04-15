using System;
using BowlingKataCore;
using NUnit.Framework;

namespace BowlingUnitTests
{
    [TestFixture]
    public class BowlingGameTests
    {
        [Test]
        public void PointsGoToNextFrameIfRollsAreFull()
        {
            var bowlingGame = new BowlingGame();

            bowlingGame.Roll();
            bowlingGame.Roll();
            bowlingGame.Roll();

            var frameTwo = bowlingGame.GetRollsForFrame(2);

            Assert.IsNotNull(frameTwo[0]);
        }

        [Test]
        public void PointAreScoreCorrectly()
        {
            var bowlingGame = new BowlingGame();

            bowlingGame.ScorePoints(7);
            bowlingGame.ScorePoints(2);

            bowlingGame.ScorePoints(9);
            bowlingGame.ScorePoints(10);

            bowlingGame.ScorePoints(10);

            bowlingGame.ScorePoints(1);
            bowlingGame.ScorePoints(3);

            bowlingGame.ScorePoints(1);
            bowlingGame.ScorePoints(1);

            var rolls = bowlingGame.GetRollsForFrame(4);

            Assert.AreEqual(1, rolls[0].Value);
            Assert.AreEqual(3, rolls[1].Value);
        }

        [Test]
        public void ThePointsinAFrameShouldNotBeMoreThan10()
        {
            var bowlingGame = new BowlingGame();

            bowlingGame.ScorePoints(9);
            bowlingGame.ScorePoints(9);

            var rolls = bowlingGame.GetRollsForFrame(1);

            Assert.AreEqual(10, rolls[0].Value + rolls[1].Value);
        }

        [Test]
        public void NotifyGameCompleteAfter10ThFrame()
        {
            var bowlingGame = new BowlingGame();

            bowlingGame.ScorePoints(7);
            bowlingGame.ScorePoints(2);

            bowlingGame.ScorePoints(9);
            bowlingGame.ScorePoints(10);

            bowlingGame.ScorePoints(4);
            bowlingGame.ScorePoints(1);

            bowlingGame.ScorePoints(1);
            bowlingGame.ScorePoints(3);

            bowlingGame.ScorePoints(1);
            bowlingGame.ScorePoints(1);

            bowlingGame.ScorePoints(10);
            bowlingGame.ScorePoints(10);
            bowlingGame.ScorePoints(10);
            bowlingGame.ScorePoints(10);

            bowlingGame.ScorePoints(10);
            bowlingGame.ScorePoints(10);
            bowlingGame.ScorePoints(10);
            var msg = bowlingGame.ScorePoints(10);

            Assert.AreEqual(BowlingMessage.TenthFrameComplete, msg);
            
        }

        [Test]
        public void ScoreCantBeLessThan0()
        {
            var bg = new BowlingGame();

            Assert.Throws<ArgumentOutOfRangeException>((() => bg.ScorePoints(-1)));
        }

        [Test]
        public void ScoreCantBeMoreThan10()
        {
            var bg = new BowlingGame();

            Assert.Throws<ArgumentOutOfRangeException>((() => bg.ScorePoints(11)));
        }

        [Test]
        public void CheckFinalScoreAllStrikes()
        {
            var bg = new BowlingGame();

            bg.ScorePoints(10); bg.ScorePoints(10); bg.ScorePoints(10);
            bg.ScorePoints(10); bg.ScorePoints(10); bg.ScorePoints(10);
            bg.ScorePoints(10); bg.ScorePoints(10); bg.ScorePoints(10);
            bg.ScorePoints(10); bg.ScorePoints(10); bg.ScorePoints(10);

            var finalScore = bg.FinalScore();

            Assert.AreEqual(300, finalScore);
        }

        [Test]
        public void CheckFinalScoreLowestPossible()
        {
            var bg = new BowlingGame();

            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);

            var finalScore = bg.FinalScore();

            Assert.AreEqual(0, finalScore);
        }


        [Test]
        public void CheckFrame10OnlyCounts2Rolls()
        {
            var bg = new BowlingGame();

            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(1); bg.ScorePoints(1);

            //Third roll in 10th frame should not be included in score
            bg.ScorePoints(1);

            var finalScore = bg.FinalScore();

            Assert.AreEqual(2, finalScore);
        }

        [Test]
        public void CheckFrame10SpareGetsThirdRoll()
        {
            var bg = new BowlingGame();

            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(1); bg.ScorePoints(9);

            //Third roll in 10th frame should not be included in score
            bg.ScorePoints(4);

            var finalScore = bg.FinalScore();

            Assert.AreEqual(14, finalScore);
        }

        [Test]
        public void CheckFrame10StrikeGetsThirdRoll()
        {
            var bg = new BowlingGame();

            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(10); bg.ScorePoints(0);

            //Third roll in 10th frame should not be included in score
            bg.ScorePoints(8);

            var finalScore = bg.FinalScore();

            Assert.AreEqual(18, finalScore);
        }

        [Test]
        public void CheckFrame10DoubleStrikeGetsThirdRoll()
        {
            var bg = new BowlingGame();

            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(0); bg.ScorePoints(0);
            bg.ScorePoints(10); bg.ScorePoints(10);

            //Third roll in 10th frame should not be included in score
            bg.ScorePoints(1);

            var finalScore = bg.FinalScore();

            Assert.AreEqual(21, finalScore);
        }
    }
}
