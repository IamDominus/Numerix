using Code.Enums;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.Gameplay;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.GSM.States
{
    public class GameplayState : IState
    {
        private readonly IPlayerTurnService _playerTurnService;
        private readonly IEventBus _eventBus;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAddService _addService;
        private readonly FieldSizeHelper _fieldSizeHelper;

        public GameplayState(IPlayerTurnService playerTurnService, IEventBus eventBus, GameStateMachine gameStateMachine, IAddService addService,
            FieldSizeHelper fieldSizeHelper)
        {
            _playerTurnService = playerTurnService;
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
            _addService = addService;
            _fieldSizeHelper = fieldSizeHelper;
        }

        public void Enter()
        {
            // _addService.CreateAndShowBanner();
            _playerTurnService.Start();
            _eventBus.Subscribe<HomeButtonClicked>(OnHomeButtonClicked);

            _fieldSizeHelper.Foo();
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