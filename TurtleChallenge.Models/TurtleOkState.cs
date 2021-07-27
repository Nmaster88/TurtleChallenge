using System;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// State for turtle when everything is ok
    /// </summary>
    public class TurtleOkState : TurtleState
    {
        public override string Text() {
            return "The turtle is Ok!"; }
    }
}
