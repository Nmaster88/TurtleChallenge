using System;
using System.Threading;
using TurtleChallenge.Models;

namespace TurtleChallenge.Services
{
    public class TurtleObserverService : IElementObserver
    {
        private Board _board;
        private PrinterService _printerService;
        public TurtleState GetLastElementState { get; set; } = new TurtleOkState();
        public Cell GetLastElementPosition { get; set; }

        public void ResetObserver(Cell Position)
        {
            GetLastElementState = new TurtleOkState();
            GetLastElementPosition = Position;
        }

        public void NoWayOut()
        {
            GetLastElementState = new TurtleNoWayOutState();
            _printerService.Print(GetLastElementState.Text());
        }

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

        private TurtleState CheckElementState(IElement element)
        {
            if (IsExit(element)) return new TurtleExitState();
            else if (IsDead(element)) return new TurtleDeadState();
            else if (IsOutOfBounds(element)) return new TurtleOutOfBoundsState();
            else if (IsDanger(element)) return new TurtleDangerState();
            else if (IsDanger(element)) return new TurtleDangerState();

            return new TurtleOkState();
        }

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

        private bool IsOutOfBounds(IElement element)
        {
            if (element.Position.x < 0 || element.Position.x >= _board.grid.width) return true;
            if (element.Position.y < 0 || element.Position.y >= _board.grid.height) return true;
            return false;
        }

        private bool IsDead(IElement element)
        {
            foreach (var mine in _board.Mines)
            {
                if (element.Position.x == mine.Position.x && element.Position.y == mine.Position.y) return true;
            }
            return false;
        }

        private bool IsExit(IElement element)
        {
            if (element.Position.x == _board.Exit.Position.x && element.Position.y == _board.Exit.Position.y) return true;
            return false;
        }
    }
}
