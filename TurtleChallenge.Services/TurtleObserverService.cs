using System;
using TurtleChallenge.Models;

namespace TurtleChallenge.Services
{
    /// <summary>
    /// turtle observer service 
    /// next method is called when a turtle action happens.
    /// it is responsible to give new states to the turtle
    /// </summary>
    public class TurtleObserverService : IObserver<Turtle>
    {
        private IDisposable unsubscriber;
        private Board _board;
        private PrinterService _printerService;

        public TurtleObserverService(Board board, PrinterService printerService)
        {
            _board = board;
            _printerService = printerService;
            _board.turtle.GetLastElementPosition = board.turtle.Position;
        }

        /// <summary>
        /// Depending on the position of the element (turtle) a state will be returned
        /// </summary>
        /// <param name="turtle"></param>
        /// <returns></returns>
        private TurtleState GetNewTurtleState(Turtle turtle)
        {
            if (IsExit(turtle))
            {
                turtle.OnExit();
            }
            else if (IsDead(turtle))
            {
                turtle.SteppedOnMine();
            }
            else if (IsOutOfBounds(turtle))
            {
                turtle.OutOfBoard();
            }
            else if (IsDanger(turtle))
            {
                turtle.NearMine();
            }
            else
            {
                turtle.Ok();
            }

            return turtle.TurtleState;
        }

        /// <summary>
        /// If the turtle is in danger, near a mine
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool IsDanger(Element element)
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
        private bool IsOutOfBounds(Element element)
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
        private bool IsDead(Element element)
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
        private bool IsExit(Element element)
        {
            if (element.Position.x == _board.Exit.Position.x && element.Position.y == _board.Exit.Position.y) return true;
            return false;
        }

        public virtual void Subscribe(IObservable<Turtle> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            this.Unsubscribe();
        }

        public virtual void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public virtual void OnNext(Turtle value)
        {
            if (_board.turtle.GetLastElementPosition != value.Position)
            {
                _printerService.PrintPositionMovement(_board.turtle.GetLastElementPosition, value.Position);
                _board.turtle.GetLastElementPosition = value.Position;
            }

            _printerService.Print(GetNewTurtleState(value).Text());
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }
}
