namespace Patterns.Observer
{
    public interface IStateSubject
    {
        void NotifyOnEnter();
        void NotifyOnExit();
    }
}
