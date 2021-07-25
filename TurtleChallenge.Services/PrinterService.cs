using System;
using System.Threading;
using TurtleChallenge.Models;

namespace TurtleChallenge.Services
{
    public class PrinterService
    {
        private static PrinterService _printerService;

        private const string Sequence = "Sequence {number}";
        private const string Dead = "Mine hit! The turtle is dead.";
        private const string OutOfBounds = "Turtle left the board!";
        private const string Danger = "The turtle is in danger!";
        private const string NoWayOut = "The turtle didn't find the way out.";
        private const string Success = "Success!";
        private const string Movement = "The turtle moved from ({fromX},{fromY}) to ({toX},{toY})";

        private PrinterService(){}

        public static PrinterService GetInstance()
        {
            return _printerService ?? (_printerService = new PrinterService());
        }

        public void PrintSequence(int number)
        {
            Console.WriteLine(Sequence.Replace("{number}",number.ToString()));
        }

        public void PrintPositionMovement(Cell position, Cell newPosition)
        {
            Console.WriteLine(Movement.Replace("{fromX}",position.x.ToString())
                .Replace("{fromY}", position.y.ToString())
                .Replace("{toX}", newPosition.x.ToString())
                .Replace("{toY}", newPosition.y.ToString()));
            ResetToDefaultColor();
        }

        public void Print (string text)
        {
            Console.WriteLine(text);
            ResetToDefaultColor();
        }

        private void ResetToDefaultColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        //public void PrintSuccess()
        //{
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine(Success);
        //    ResetToDefaultColor();
        //}

        //public void PrintDanger()
        //{
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    Console.WriteLine(Danger);
        //    ResetToDefaultColor();
        //}

        //public void PrintOutOfBounds()
        //{
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine(OutOfBounds);
        //    ResetToDefaultColor();
        //}

        //public void PrintDead()
        //{
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine(Dead);
        //    ResetToDefaultColor();
        //}

        public void PrintNoWayOut()
        {
            Console.WriteLine(NoWayOut);
        }
    }
}
