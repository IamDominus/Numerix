using Code.Enums;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Services;

namespace Code.Infrastructure.GSM.States
{
    public class GameplayState : IState
    {
        private readonly IEventBus _eventBus;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAddService _addService;
        private readonly IInputService _inputService;

        public GameplayState(IEventBus eventBus, GameStateMachine gameStateMachine, IAddService addService, IInputService inputService)
        {
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
            _addService = addService;
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.Enable();
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
            _addService.DestroyBanner();
            _eventBus.Unsubscribe<HomeButtonClicked>(OnHomeButtonClicked);
        }
    }
}