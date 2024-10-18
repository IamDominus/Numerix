using System;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.States;
using Zenject;

namespace Code.Infrastructure.GSM.StateRegistries
{
    public class MainMenuStateRegistry : IInitializable, IDisposable
    {
        private readonly StateFactory _stateFactory;
        private readonly GameStateMachine _gameStateMachine;

        public MainMenuStateRegistry(GameStateMachine gameStateMachine, StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.RegisterState(_stateFactory.Create<MainMenuState>());
        }

        public void Dispose()
        {
            _gameStateMachine.UnregisterState<MainMenuState>();
        }
    }
}