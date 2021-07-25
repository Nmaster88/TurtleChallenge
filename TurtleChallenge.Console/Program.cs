using System;
using TurtleChallenge.Services;

namespace TurtleChallenge.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string settingsFileName = null;
            string moveSequencesFileName = null;
            if(args.Length == 1)
            {
                settingsFileName = args[0];
            }
            else if(args.Length >= 2)
            {
                settingsFileName = args[0];
                moveSequencesFileName = args[1];
            }
            GameService game = new GameService(settingsFileName, moveSequencesFileName);
            game.ExecuteGame();
            System.Console.ReadKey();
        }
    }
}
