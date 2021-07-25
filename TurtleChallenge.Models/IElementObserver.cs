namespace TurtleChallenge.Models
{
    public interface IElementObserver
    {
        // Receive update from subject
        void Update(IElement subject);
    }
}
