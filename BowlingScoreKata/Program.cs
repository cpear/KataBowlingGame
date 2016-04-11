using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreKata
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class BowlingGame
    {

        public void Roll()
        {

        }

        public int FinalScore()
        {
            return 0;
        }
    }


    public class Frame
    {
        public int?[] Rolls { get; private set; }
        public bool IsTenthFrame => Rolls.Length == 3;


        public Frame(bool isTenthframe = false)
        {
            Rolls = isTenthframe ? new int?[3] : new int?[2];
        }

        public void Score(int pointsToScore)
        {
            if(pointsToScore > 10) { throw new ArgumentOutOfRangeException(nameof(pointsToScore), "The score must be less than 11");}

            if (Rolls[0] == null)
            {
                Rolls[0] = pointsToScore;
                return;
            }

            if (Rolls[0] < 10 && Rolls[1] == null)
            {
                Rolls[1] = pointsToScore;
                return;
            }

            if (IsTenthFrame && Rolls[2] == null)
            {
                Rolls[2] = pointsToScore;
            }
        }
    }
}