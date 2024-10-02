using Zenject;

namespace Code.Infrastructure.FSM
{
    public class StateFactory
    {
        private readonly DiContainer _diContainer;

        public StateFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        //TODO investigate .Instantiate instead of .Resolve
        public TState Create<TState>() where TState : IExitableState
        {
            return _diContainer.Resolve<TState>();
        }
    }
}