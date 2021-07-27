namespace TurtleChallenge.Models
{
    /// <summary>
    /// IElementObserver is an interface to be used by observers classes.
    /// That will receive notifications from an IElement class when attached to it.
    /// </summary>
    public interface IElementObserver
    {
        // Receive update from subject
        void Update(IElement subject);
    }
}
