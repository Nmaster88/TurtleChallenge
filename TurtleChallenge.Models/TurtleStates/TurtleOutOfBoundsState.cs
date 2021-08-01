using System;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// State for turtle out of the board
    /// </summary>
    public class TurtleOutOfBoundsState : TurtleState
    {
        public override string Text() {
            Console.ForegroundColor = ConsoleColor.Red;
            return "Turtle left the board!"; }
    }
}
