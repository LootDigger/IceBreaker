namespace Patterns.Observer
{
    public interface IStateObserver
    {
        void OnSubjectStateEnter(IStateSubject _stateSubject);
        void OnSubjectStateExit(IStateSubject _stateSubject);
    }
}
