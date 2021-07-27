using System;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// Base class for the different turtle states
    /// </summary>
    public abstract class TurtleState
    {
        public virtual string Text() { return ""; }
    }
}
