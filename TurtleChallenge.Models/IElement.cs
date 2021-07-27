using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// IElement interface, has a Cell position property and the methods for any class that inherits this to be observable
    /// by another class that implements the IElementObserver interface
    /// </summary>
    public interface IElement
    {
        Cell Position { get; set; }

        // Attach an observer to the subject.
        void Attach(IElementObserver observer);

        // Detach an observer from the subject.
        void Detach(IElementObserver observer);

        // Notify all observers about an event.
        void Notify();
    }
}
