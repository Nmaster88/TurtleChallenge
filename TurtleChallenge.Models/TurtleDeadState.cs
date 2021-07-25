using System;

namespace TurtleChallenge.Models
{
    public class TurtleDeadState : TurtleState
    {
        public override string Text() {
            Console.ForegroundColor = ConsoleColor.Red;
            return "Mine hit! The turtle is dead."; }
    }
}
