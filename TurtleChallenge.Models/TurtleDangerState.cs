using System;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// State for turtle when it is in danger
    /// </summary>
    public class TurtleDangerState : TurtleState
    {
        public override string Text() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return "The turtle is in danger!"; }
    }
}
