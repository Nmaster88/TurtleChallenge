using System;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// State for turtle when it is dead
    /// </summary>
    public class TurtleDeadState : TurtleState
    {
        public override string Text() {
            Console.ForegroundColor = ConsoleColor.Red;
            return "Mine hit! The turtle is dead."; }
    }
}
