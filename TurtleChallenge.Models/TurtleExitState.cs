using System;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// State for turtle when it exits
    /// </summary>
    public class TurtleExitState : TurtleState
    {
        public override string Text() {
            Console.ForegroundColor = ConsoleColor.Green;
            return "Turtle found the exit!"; }
    }
}
