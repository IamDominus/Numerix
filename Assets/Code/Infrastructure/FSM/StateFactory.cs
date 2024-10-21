using Zenject;

namespace Code.Infrastructure.FSM
{
    public class StateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public TState Create<TState>() where TState : IExitableState
        {
            return _instantiator.Instantiate<TState>();
        }
    }
}