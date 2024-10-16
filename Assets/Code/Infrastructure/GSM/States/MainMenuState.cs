using Code.Enums;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Services;
using Code.Services.Ad;
using Code.ViewControllers;

namespace Code.Infrastructure.GSM.States
{
    public class MainMenuState : IState
    {
        private readonly IViewController _mainMenuViewController;
        private readonly IEventBus _eventBus;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAdService _adService;

        public MainMenuState(MainMenuViewController mainMenuViewController, IEventBus eventBus, GameStateMachine gameStateMachine, IAdService adService)
        {
            _mainMenuViewController = mainMenuViewController;
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
            _adService = adService;
        }

        public void Enter()
        {
            _adService.CreateAndShowBanner();
            _eventBus.Subscribe<PlayButtonClicked>(OnPlayButtonClicked);
            _mainMenuViewController.Show();
        }

        public void Exit()
        {
            _adService.DestroyBanner();
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