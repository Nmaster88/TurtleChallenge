using System;
using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    public class Moves
    {
        private static Moves _moves;

        private List<string[]> _sequences;

        //public Moves()
        //{
        //    _sequences = new List<string[]>();
        //}
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

        public void AddSequences(List<string[]> sequences)
        {
            _sequences = sequences;
        }

        public List<string[]> GetSequences()
        {
            return _sequences;
        }
    }
}
