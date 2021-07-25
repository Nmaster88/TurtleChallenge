using System;

namespace TurtleChallenge.Models
{
    public class TurtleExitState : TurtleState
    {
        public override string Text() {
            Console.ForegroundColor = ConsoleColor.Green;
            return "Turtle found the exit!"; }
    }
}
