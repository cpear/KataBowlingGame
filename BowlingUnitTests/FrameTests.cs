using System;
using BowlingKataCore;
using NUnit.Framework;

namespace BowlingUnitTests
{
    [TestFixture]
    public class FrameTests
    {
        [Test]
        public void NoMoreScoresIfFirstScoreIs10()
        {
            var bowlingGame = new BowlingGame();

            bowlingGame.ScorePoints(10);
            bowlingGame.ScorePoints(5);

            Assert.AreEqual(10, bowlingGame.GetRollsForFrame(1)[0]);
            Assert.IsNull(bowlingGame.GetRollsForFrame(1)[1]);
        }

        [Test]
        public void SaveOnlyTwoScoresNormalFrame()
        {
            var bowlingGame = new BowlingGame();

            bowlingGame.ScorePoints(8);
            bowlingGame.ScorePoints(1);
            bowlingGame.ScorePoints(9);

            Assert.AreEqual(2, bowlingGame.GetRollsForFrame(1).Length);
            Assert.AreEqual(8, bowlingGame.GetRollsForFrame(1)[0]);
            Assert.AreEqual(1, bowlingGame.GetRollsForFrame(1)[1]);
        }

        [Test]
        public void SaveThreeScoreTenthFrame()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.ScorePoints(10); bowlingGame.ScorePoints(10); bowlingGame.ScorePoints(10);
            bowlingGame.ScorePoints(10); bowlingGame.ScorePoints(10); bowlingGame.ScorePoints(10);
            bowlingGame.ScorePoints(10); bowlingGame.ScorePoints(10); bowlingGame.ScorePoints(10);

            bowlingGame.ScorePoints(8); 
            bowlingGame.ScorePoints(7);
            bowlingGame.ScorePoints(9);

            Assert.AreEqual(3, bowlingGame.GetRollsForFrame(10).Length);
            Assert.AreEqual(8, bowlingGame.GetRollsForFrame(10)[0]);
            Assert.AreEqual(7, bowlingGame.GetRollsForFrame(10)[1]);
            Assert.AreEqual(9, bowlingGame.GetRollsForFrame(10)[2]);
        }

        [Test]
        public void ErrorIfScoreMoreThan10FirstThrow()
        {
            var bowlingGame = new BowlingGame();

            Assert.Throws<ArgumentOutOfRangeException>(() => bowlingGame.ScorePoints(11));          
        }

        [Test]
        public void CheckIsTenthFrame()
        {
            var frame = new Frame(true);
            Assert.IsTrue(frame.IsTenthFrame);

        }

        [Test]
        public void CheckIsNotTenthFrame()
        {
            var frame = new Frame();

            Assert.IsFalse(frame.IsTenthFrame);
        }
    }
}
