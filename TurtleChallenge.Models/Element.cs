﻿using System;
using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    public class Element : IElement
    {
        public Cell Position { get; set; }

        private List<IElementObserver> _observers = new List<IElementObserver>();

        public void Attach(IElementObserver observer)
        {
            this._observers.Add(observer);
        }

        public void Detach(IElementObserver observer)
        {
            this._observers.Remove(observer);
        }

        public void Notify()
        {
            if (_observers.Count == 0)
                return;

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
}
