using System;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// State for turtle when it didn't find the way out of the board. No more moves for a given sequence.
    /// </summary>
    public class TurtleNoWayOutState : TurtleState
    {
        public override string Text() { return "The turtle didn't find the way out."; }
    }
}
