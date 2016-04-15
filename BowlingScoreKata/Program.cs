using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowlingKataCore;

namespace BowlingScoreKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var bowlingGame = new BowlingGame();

            //Max rolls per game is 21
            for (int x = 0; x <= 21; x++)
            {
                bowlingGame.Roll();
            }

            for (int i = 1; i <= 10; i++)
            {
                var rolls = bowlingGame.GetRollsForFrame(i);

                Console.WriteLine("Frame " + i);
                Console.WriteLine("R1: " + rolls[0]);
                Console.WriteLine("R2: " + rolls[1]);
                if(i == 10) Console.WriteLine("R3: " + rolls[2]);
                Console.WriteLine("--------------------");
            }
            Console.WriteLine();
            Console.WriteLine();
            
            Console.WriteLine("Final Score: " + bowlingGame.FinalScore());
            Console.WriteLine();

        }
    }
}