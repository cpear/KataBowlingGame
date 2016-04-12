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
            var frame = new Frame();

            frame.Score(10);
            frame.Score(5);

            Assert.AreEqual(10, frame.Rolls[0]);
            Assert.IsNull(frame.Rolls[1]);
        }

        [Test]
        public void SaveOnlyTwoScoresNormalFrame()
        {
            var frame = new Frame();

            frame.Score(8);
            frame.Score(7);
            frame.Score(9);

            Assert.AreEqual(2, frame.Rolls.Length);
            Assert.AreEqual(8, frame.Rolls[0]);
            Assert.AreEqual(7, frame.Rolls[1]);
        }

        [Test]
        public void SaveThreeScoreTenthFrame()
        {
            var frame = new Frame(true);

            frame.Score(8);
            frame.Score(7);
            frame.Score(9);

            Assert.AreEqual(3, frame.Rolls.Length);
            Assert.AreEqual(8, frame.Rolls[0]);
            Assert.AreEqual(7, frame.Rolls[1]);
            Assert.AreEqual(9, frame.Rolls[2]);
        }

        [Test]
        public void ErrorIfScoreMoreThan10FirstThrow()
        {
            var frame = new Frame();

            Assert.Throws<ArgumentOutOfRangeException>(() => frame.Score(11));          
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
