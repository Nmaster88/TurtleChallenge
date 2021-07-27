using System;
using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// Board of the game, with all related properties.
    /// </summary>
    public class Board
    {
        public Grid grid { get; set; }
        
        public Turtle turtle { get; set; }
        
        public List<Mine> Mines { get; set; }

        public Exit Exit { get; set; }
    }

}
