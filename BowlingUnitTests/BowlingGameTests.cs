using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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

    }
}
