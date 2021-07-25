using System;

namespace TurtleChallenge.Models
{
    public class Turtle : Element
    {
        private static Turtle _turtle;

        private Turtle(Cell position) { Position = position; }

        public static Turtle GetInstance(Cell position)
        {
            return _turtle ?? (_turtle = new Turtle(position));
        }
        
        public Dir Direction { get; set; }

        public TurtleState TurtleState { get; set; }

        public void ChangeTurtleState(TurtleState state)
        {
            TurtleState = state;
        }

        /// <summary>
        /// Moves the turtle to a new position depending on the direction
        /// </summary>
        public void Move()
        {
            //TODO validations, only allow Move if the turtle is OK
            switch(Direction)
            {
                case Dir.NORTH:
                    Position = new Cell { x = Position.x, y = Position.y - 1 };
                    break;
                case Dir.EAST:
                    Position = new Cell { x = Position.x + 1, y = Position.y };
                    break;
                case Dir.SOUTH:
                    Position = new Cell { x = Position.x, y = Position.y + 1 };
                    break;
                case Dir.WEST:
                    Position = new Cell { x = Position.x - 1, y = Position.y };
                    break;
            }

            Notify();
        }

        /// <summary>
        /// Rotates the turtle to another direction by 90º to the right
        /// </summary>
        public void Rotate()
        {
            //TODO validations, only allow Rotation if the turtle is OK
            switch (Direction)
            {
                case Dir.NORTH:
                    Direction = Dir.EAST;
                    break;
                case Dir.EAST:
                    Direction = Dir.SOUTH;
                    break;
                case Dir.SOUTH:
                    Direction = Dir.WEST;
                    break;
                case Dir.WEST:
                    Direction = Dir.NORTH;
                    break;
            }

            //Notify();
        }
    }

    public enum Dir { NORTH, SOUTH, EAST, WEST }

    //public enum ElementState { IsDead, IsExit, IsOutOfBounds, IsDanger, IsOk
}
