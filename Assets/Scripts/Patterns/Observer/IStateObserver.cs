namespace Patterns.Observer
{
    public interface IStateObserver
    {
        void OnSubjectStateEnter(IStateSubject stateSubject);
        void OnSubjectStateExit(IStateSubject stateSubject);
    }
}
