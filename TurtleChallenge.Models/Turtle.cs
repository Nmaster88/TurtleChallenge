using System;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// Turtle is the element that moves on the board.
    /// </summary>
    public class Turtle : Element
    {
        public Cell GetLastElementPosition { get; set; }

        public Dir GetLastDirection { get; set; }

        private TurtleState InitialElementState { get; set; } = new TurtleOkState();

        private Cell InitialElementPosition { get; set; }

        private Dir InitialDirection { get; set; }

        public Dir Direction { get; set; }

        public TurtleState TurtleState { get; private set; }

        private static Turtle _turtle;

        private Turtle(Cell position, Dir direction) { 
            Position = position;
            InitialElementPosition = position;
            InitialElementState = new TurtleOkState();
            InitialDirection = direction;
            GetLastDirection = direction;
            GetLastElementPosition = position;
        }

        public static Turtle GetInstance(Cell position, Dir direction)
        {
            return _turtle ?? (_turtle = new Turtle(position, direction));
        }

        public void SteppedOnMine()
        {
            TurtleState = new TurtleDeadState();
        }

        public void NearMine()
        {
            TurtleState = new TurtleDangerState();
        }

        public void OutOfBoard()
        {
            TurtleState = new TurtleOutOfBoundsState();
        }

        public void OnExit()
        {
            TurtleState = new TurtleExitState();
        }

        public void Ok()
        {
            TurtleState = new TurtleOkState();
        }

        public void DidNotFoundExit()
        {
            TurtleState = new TurtleNoWayOutState();
        }

        public void ResetTurtle()
        {
            Position = InitialElementPosition;
            Direction = InitialDirection;
            TurtleState = new TurtleOkState();
        }

        public bool IsTurtleAlive()
        {
            return _turtle.TurtleState.GetType() == typeof(TurtleDangerState) || _turtle.TurtleState.GetType() == typeof(TurtleOkState);
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
        }
    }

    public enum Dir { NORTH, SOUTH, EAST, WEST }
}
