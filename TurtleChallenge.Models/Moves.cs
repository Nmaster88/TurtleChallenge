using System;
using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// Moves is an object that contains the sequences for movements and rotating of turtle element.
    /// </summary>
    public class Moves
    {
        private static Moves _moves;

        private List<string[]> _sequences;

        private Moves()
        {
            _sequences = new List<string[]>();
        }

        public static Moves GetInstance()
        {
            return _moves ?? (_moves = new Moves());
        }

        public void AddSequence(string[] sequence)
        {
            _sequences.Add(sequence);
        }

        public List<string[]> GetSequences()
        {
            return _sequences;
        }
    }
}
