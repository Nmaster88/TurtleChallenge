using System;
using System.Threading;
using TurtleChallenge.Models;

namespace TurtleChallenge.Services
{
    /// <summary>
    /// Responsive for printing to the console
    /// </summary>
    public class PrinterService
    {
        private static PrinterService _printerService;

        private const string Sequence = "Sequence {number}";
        private const string Movement = "The turtle moved from ({fromX},{fromY}) to ({toX},{toY})";

        private PrinterService(){}

        public static PrinterService GetInstance()
        {
            return _printerService ?? (_printerService = new PrinterService());
        }

        /// <summary>
        /// Prints a sequence number
        /// </summary>
        /// <param name="number"></param>
        public void PrintSequence(int number)
        {
            Console.WriteLine(Sequence.Replace("{number}",number.ToString()));
        }

        /// <summary>
        /// Prints the movement of the turtle
        /// </summary>
        /// <param name="position"></param>
        /// <param name="newPosition"></param>
        public void PrintPositionMovement(Cell position, Cell newPosition)
        {
            Console.WriteLine(Movement.Replace("{fromX}",position.x.ToString())
                .Replace("{fromY}", position.y.ToString())
                .Replace("{toX}", newPosition.x.ToString())
                .Replace("{toY}", newPosition.y.ToString()));
            ResetToDefaultColor();
        }

        /// <summary>
        /// Generic printing depending on the argument
        /// </summary>
        /// <param name="text"></param>
        public void Print (string text)
        {
            Console.WriteLine(text);
            ResetToDefaultColor();
        }
        
        /// <summary>
        /// Resets the color of printing test to white
        /// </summary>
        private void ResetToDefaultColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
