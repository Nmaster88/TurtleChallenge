using System;

namespace TurtleChallenge.Models
{
    public class TurtleOutOfBoundsState : TurtleState
    {
        public override string Text() {
            Console.ForegroundColor = ConsoleColor.Red;
            return "Turtle left the board!"; }
    }
}
