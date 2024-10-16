using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.States;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private StateFactory _stateFactory;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, StateFactory stateFactory)
        {
            _gameStateMachine = gameStateMachine;
            _stateFactory = stateFactory;
        }

        private void Start()
        {
            _gameStateMachine.RegisterState(_stateFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadGameState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadSceneState>());

            _gameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}