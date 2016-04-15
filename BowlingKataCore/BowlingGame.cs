using System;
using System.Linq;
using System.Threading;

namespace BowlingKataCore
{
    public enum BowlingMessage
    {
        Fail, Success, TenthFrameComplete
    }

    public class BowlingGame
    {
        private readonly Frame[] _frames;
        private int _currentFrameCounter;

        private Frame CurrentFrame => _frames[_currentFrameCounter];

        public BowlingGame()
        {
            //Really?
            _frames = new Frame[10]
            {
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(true)
            };

            _currentFrameCounter = 0;
        }

        private void HandleFirstRollOfFrame()
        {
            if (CurrentFrame.IsTenthFrame) return;

            if (CurrentFrame.Rolls[0] != 10) return;

            _currentFrameCounter++;
        }

        private void HandleSecondRollOfFrame()
        {
            if (CurrentFrame.IsTenthFrame) return;

            _currentFrameCounter++;
        }

        /// <summary>
        /// Shave the points on a second roll if (roll1 + roll2) is greatre than 10 
        /// </summary>
        /// <param name="points">Points scored on a second roll</param>
        /// <returns>A valid value for the second roll</returns>
        private int ShavePoints(int points)
        {
            if (CurrentFrame.IsTenthFrame) return points;

            if (CurrentFrame.Rolls[0].Value == 0) return points;

            if (CurrentFrame.Rolls[0].Value + points <= 10) return points;

            return Math.Abs(CurrentFrame.Rolls[0].Value - 10);

        }

        private BowlingMessage Score10thFramePoints(Frame thisFrame, int pointsToScore)
        {
            //First Roll 
            if (thisFrame.Rolls[0] == null)
            {
                thisFrame.Rolls[0] = pointsToScore;

                return BowlingMessage.Success;
            }

            //2nd Roll
            if (thisFrame.Rolls[1] == null)
            {
                thisFrame.Rolls[1] = pointsToScore;

                return BowlingMessage.Success;
            }

            //3rd Roll
            if (thisFrame.Rolls[2] == null && thisFrame.Rolls[0] + thisFrame.Rolls[1] >= 10)
            {
                thisFrame.Rolls[2] = pointsToScore;

                return BowlingMessage.Success;
            }

            thisFrame.Rolls[2] = 0;

            return BowlingMessage.TenthFrameComplete;
        }

        public BowlingMessage ScorePoints(int pointsToScore)
        {
            if (pointsToScore > 10) { throw new ArgumentOutOfRangeException(nameof(pointsToScore), "The score must be less than 11"); }
            if (pointsToScore < 0) { throw new ArgumentOutOfRangeException(nameof(pointsToScore), "The score must be more than -1"); }

            var thisFrame = _frames[_currentFrameCounter];

            //Handle 10th frame
            if (thisFrame.IsTenthFrame) return Score10thFramePoints(thisFrame, pointsToScore);

            //First Roll Normal Frame
            if (thisFrame.Rolls[0] == null)
            {
                thisFrame.Rolls[0] = pointsToScore;

                HandleFirstRollOfFrame();
                return BowlingMessage.Success;
            }

            //Second Roll Normal Frame
            if (thisFrame.Rolls[0] < 10 && thisFrame.Rolls[1] == null)
            {
                thisFrame.Rolls[1] = ShavePoints(pointsToScore);

                HandleSecondRollOfFrame();
                return BowlingMessage.Success;
            }

            return BowlingMessage.Fail;
        }

        public int?[] GetRollsForFrame(int frame)
        {
            return _frames[frame - 1].Rolls;
        }

        /// <summary>
        /// Generates a random score for a single roll and saves it.
        /// </summary>
        /// <returns></returns>
        public BowlingMessage Roll()
        {
            Random rnd = new Random();
            Thread.Sleep(13);
            int points = rnd.Next(0, 11);

            return ScorePoints(points);
        }

        public int FinalScore()
        {
            int total = 0;

            for (int i = 0; i <= 8; i++)
            {
                var firstRoll = _frames[i].Rolls[0].Value;
                var secondRoll = _frames[i].Rolls[1] == null ? 0 : _frames[i].Rolls[1].Value;


                if (firstRoll == 10)
                {
                    //We got a strike
                    total = total + 10 + GetFirstRollAftercurrentRoll(i) + GetSecondRollAfterCurrentRoll(i);
                }
                else if (firstRoll + secondRoll == 10)
                {
                    //We got a spare
                    var nextFrameRolls = _frames[i + 1].Rolls;
                    total = total + 10 + nextFrameRolls[0].Value;
                }
                else
                {
                    total = total + firstRoll + secondRoll;
                }

            }

            return total + _frames[9].Rolls.Sum(i => i.GetValueOrDefault(0));
        }

        private int GetFirstRollAftercurrentRoll(int currentFrame )
        {
            return  _frames[currentFrame + 1].Rolls[0].Value;
        }

        private int GetSecondRollAfterCurrentRoll(int currentFrame)
        {
            var nextFrame = _frames[currentFrame + 1];

            return nextFrame.Rolls[1] ?? _frames[currentFrame + 2].Rolls[0].Value;
        }

    }
}