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


    }
}
