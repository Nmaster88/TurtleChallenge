using System;
using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// Turtle is the element that moves on the board.
    /// </summary>
    public class TurtleObservable : IObservable<Turtle>
    {
        private static Turtle _turtle;

        public TurtleObservable(Turtle turtle)
        {
            _turtle = turtle;
            observers = new List<IObserver<Turtle>>();
        }

        private List<IObserver<Turtle>> observers;

        public IDisposable Subscribe(IObserver<Turtle> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Turtle>> _observers;
            private IObserver<Turtle> _observer;

            public Unsubscriber(List<IObserver<Turtle>> observers, IObserver<Turtle> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void TrackTurtle(Turtle turtle)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(turtle);
            }
        }
    }
}
