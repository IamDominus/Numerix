using Code.Enums;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;
using Code.ViewControllers;

namespace Code.Infrastructure.GSM.States
{
    public class MainMenuState : IState
    {
        private readonly IViewController _mainMenuViewController;
        private readonly IEventBus _eventBus;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAddService _addService;

        public MainMenuState(MainMenuViewController mainMenuViewController, IEventBus eventBus, GameStateMachine gameStateMachine, IAddService addService)
        {
            _mainMenuViewController = mainMenuViewController;
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
            _addService = addService;
        }

        public void Enter()
        {
            _addService.CreateAndShowBanner();
            _eventBus.Subscribe<PlayButtonClicked>(OnPlayButtonClicked);
            _mainMenuViewController.Show();
        }

        public void Exit()
        {
            _addService.DestroyBanner();
            _eventBus.Unsubscribe<PlayButtonClicked>(OnPlayButtonClicked);
            _mainMenuViewController.Hide();
        }

        private void OnPlayButtonClicked()
        {
            var payload = new LoadScenePayload()
            {
                SceneName = SceneName.Level,
                Callback = () => _gameStateMachine.Enter<ConstructLevelState>()
            };
            _gameStateMachine.Enter<LoadSceneState, LoadScenePayload>(payload);
        }
    }
}