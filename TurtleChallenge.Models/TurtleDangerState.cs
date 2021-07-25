using System;

namespace TurtleChallenge.Models
{
    public class TurtleDangerState : TurtleState
    {
        public override string Text() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return "The turtle is in danger!"; }
    }
}
