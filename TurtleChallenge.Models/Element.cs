using System;
using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// Element is an entity, that has a Cell position
    /// and implements the IElement interface
    /// which allows the element to be observed.
    /// </summary>
    public class Element
    {
        public Cell Position { get; set; }
    }
}
