using System;

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

        public BowlingMessage ScorePoints(int pointsToScore)
        {
            if (pointsToScore > 10) { throw new ArgumentOutOfRangeException(nameof(pointsToScore), "The score must be less than 11"); }
            if (pointsToScore < 0) { throw new ArgumentOutOfRangeException(nameof(pointsToScore), "The score must be more than -1"); }

            var thisFrame = _frames[_currentFrameCounter];

            //First Roll
            if (thisFrame.Rolls[0] == null)
            {
                thisFrame.Rolls[0] = pointsToScore;

                HandleFirstRollOfFrame();
                return BowlingMessage.Success;
            }

            //Second Roll
            if (thisFrame.Rolls[0] < 10 && thisFrame.Rolls[1] == null)
            {
                thisFrame.Rolls[1] = ShavePoints(pointsToScore);

                HandleSecondRollOfFrame();
                return BowlingMessage.Success;
            }

            //3rd Roll - Tenth Frame Only
            if (thisFrame.IsTenthFrame && thisFrame.Rolls[2] == null)
            {
                thisFrame.Rolls[2] = pointsToScore;

                return BowlingMessage.Success;
            }

            return BowlingMessage.TenthFrameComplete;
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
            int points = rnd.Next(0, 11);

            return ScorePoints(points);
        }

        public int FinalScore()
        {
            return 0;
        }
    }
}