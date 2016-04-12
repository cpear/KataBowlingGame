using System;

namespace BowlingKataCore
{
    public enum BowlingMessage
    {
        Fail, Success, TenthFrameCompleteScoreNotSaved
    }

    public class BowlingGame
    {
        private readonly Frame[] _frames;
        private int _currentFrame;

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
                new Frame()
            };

            _currentFrame = 0;
        }

        private void HandleFirstRollOfFrame()
        {
            var thisFrame = _frames[_currentFrame];

            if (thisFrame.IsTenthFrame) return;

            if (thisFrame.Rolls[0] != 10) return;

            _currentFrame++;
        }

        private void HandleSecondRollOfFrame()
        {
            var thisFrame = _frames[_currentFrame];

            if (thisFrame.IsTenthFrame) return;

            _currentFrame++;
        }

        private int ShavePoints()
        {
            //Shavepoints off of second roll. You can only reach a cap of 10
            return 0;
        }

        private BowlingMessage ScorePoints(int pointsToScore)
        {
            if (pointsToScore > 10) { throw new ArgumentOutOfRangeException(nameof(pointsToScore), "The score must be less than 11"); }
            if (pointsToScore < 0) { throw new ArgumentOutOfRangeException(nameof(pointsToScore), "The score must be more than -1"); }

            var thisFrame = _frames[_currentFrame];

            if (thisFrame.Rolls[0] == null)
            {
                thisFrame.Rolls[0] = pointsToScore;

                HandleFirstRollOfFrame();
                return BowlingMessage.Success;
            }

            if (thisFrame.Rolls[0] < 10 && thisFrame.Rolls[1] == null)
            {
                thisFrame.Rolls[1] = pointsToScore;

                HandleSecondRollOfFrame();
                return BowlingMessage.Success;
            }

            if (thisFrame.IsTenthFrame && thisFrame.Rolls[2] == null)
            {
                thisFrame.Rolls[2] = pointsToScore;

                _currentFrame++;
                return BowlingMessage.Success;
            }

            return BowlingMessage.Fail;
        }

        public int?[] GetRollsForFrame(int frame)
        {
            return _frames[frame - 1].Rolls;
        }

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