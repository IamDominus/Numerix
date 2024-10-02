namespace Code.Infrastructure.FSM
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}