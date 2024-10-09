using Code.Enums;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.Gameplay;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;

namespace Code.Infrastructure.GSM.States
{
    public class GameplayState : IState
    {
        private readonly IPlayerTurnService _playerTurnService;
        private readonly IEventBus _eventBus;
        private readonly GameStateMachine _gameStateMachine;

        public GameplayState(IPlayerTurnService playerTurnService, IEventBus eventBus, GameStateMachine gameStateMachine)
        {
            _playerTurnService = playerTurnService;
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _playerTurnService.Start();
            _eventBus.Subscribe<HomeButtonClicked>(OnHomeButtonClicked);
        }

        private void OnHomeButtonClicked()
        {
            var payload = new LoadScenePayload()
            {
                SceneName = SceneName.MainMenu,
                Callback = () => _gameStateMachine.Enter<MainMenuState>()
            };
            _gameStateMachine.Enter<LoadSceneState, LoadScenePayload>(payload);
        }

        public void Exit()
        {
            _eventBus.Unsubscribe<HomeButtonClicked>(OnHomeButtonClicked);
        }
    }
}