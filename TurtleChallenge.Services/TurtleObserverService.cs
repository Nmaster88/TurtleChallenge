using System;
using System.Threading;
using TurtleChallenge.Models;

namespace TurtleChallenge.Services
{
    /// <summary>
    /// turtle observer service 
    /// update method is called by the element it observes.
    /// </summary>
    public class TurtleObserverService : IElementObserver
    {
        private Board _board;
        private PrinterService _printerService;
        public TurtleState GetLastElementState { get; set; } = new TurtleOkState();
        public Cell GetLastElementPosition { get; set; }

        /// <summary>
        /// Reset the Properties that contain the state and position to
        /// values passed by argument
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="State"></param>
        public void ResetObserver(Cell Position, TurtleState State)
        {
            GetLastElementState = State;
            GetLastElementPosition = Position;
        }

        /// <summary>
        /// Calls no way out text
        /// </summary>
        public void NoWayOut()
        {
            GetLastElementState = new TurtleNoWayOutState();
            _printerService.Print(GetLastElementState.Text());
        }

        /// <summary>
        /// boolean where value depends if the turtle is alive.
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return GetLastElementState.GetType() == typeof(TurtleDangerState) || GetLastElementState.GetType() == typeof(TurtleOkState);
        }

        public TurtleObserverService(Board board, PrinterService printerService)
        {
            _board = board;
            _printerService = printerService;
            GetLastElementPosition = board.turtle.Position;
        }

        /// <summary>
        /// Update method that is called by the an Element is being observed.
        /// </summary>
        /// <param name="element"></param>
        public void Update(IElement element)
        {
            if(GetLastElementPosition != element.Position)
            {
                _printerService.PrintPositionMovement(GetLastElementPosition, element.Position);
                GetLastElementPosition = element.Position;
            }

            GetLastElementState = CheckElementState(element);
            _board.turtle.ChangeTurtleState(GetLastElementState);
            _printerService.Print(GetLastElementState.Text());
        }

        /// <summary>
        /// Depending on the position of the element (turtle) a state will be returned
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private TurtleState CheckElementState(IElement element)
        {
            if (IsExit(element)) return new TurtleExitState();
            else if (IsDead(element)) return new TurtleDeadState();
            else if (IsOutOfBounds(element)) return new TurtleOutOfBoundsState();
            else if (IsDanger(element)) return new TurtleDangerState();
            else if (IsDanger(element)) return new TurtleDangerState();

            return new TurtleOkState();
        }

        /// <summary>
        /// If the turtle is in danger, near a mine
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool IsDanger(IElement element)
        {
            foreach(var mine in _board.Mines)
            {
                if (element.Position.x == (mine.Position.x - 1) && element.Position.y == (mine.Position.y)) return true;
                if (element.Position.x == (mine.Position.x) && element.Position.y == (mine.Position.y-1)) return true;
                if (element.Position.x == (mine.Position.x+1) && element.Position.y == (mine.Position.y)) return true;
                if (element.Position.x == (mine.Position.x) && element.Position.y == (mine.Position.y+1)) return true;
                if (element.Position.x == (mine.Position.x-1) && element.Position.y == (mine.Position.y-1)) return true;
                if (element.Position.x == (mine.Position.x+1) && element.Position.y == (mine.Position.y+1)) return true;
            }
            return false;
        }

        /// <summary>
        /// If the turtle is out of the board
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool IsOutOfBounds(IElement element)
        {
            if (element.Position.x < 0 || element.Position.x >= _board.grid.width) return true;
            if (element.Position.y < 0 || element.Position.y >= _board.grid.height) return true;
            return false;
        }

        /// <summary>
        /// If the turtle is dead, because it stepped on a mine
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool IsDead(IElement element)
        {
            foreach (var mine in _board.Mines)
            {
                if (element.Position.x == mine.Position.x && element.Position.y == mine.Position.y) return true;
            }
            return false;
        }

        /// <summary>
        /// If the turtle is in the Exit element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool IsExit(IElement element)
        {
            if (element.Position.x == _board.Exit.Position.x && element.Position.y == _board.Exit.Position.y) return true;
            return false;
        }
    }
}
