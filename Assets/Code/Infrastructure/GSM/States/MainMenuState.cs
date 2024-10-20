using Code.Enums;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Services.Ad;
using Code.Services.BackButton;
using Code.Services.BackButton.Workers;
using Code.ViewControllers;

namespace Code.Infrastructure.GSM.States
{
    public class MainMenuState : IState
    {
        private readonly IViewController _mainMenuViewController;
        private readonly IEventBus _eventBus;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAdService _adService;
        private readonly IBackButtonService _backButtonService;
        private readonly IBackButtonWorker _backButtonWorker;

        public MainMenuState(MainMenuViewController mainMenuViewController, IEventBus eventBus, GameStateMachine gameStateMachine, IAdService adService,
            IBackButtonService backButtonService, MainMenuBackButtonWorker backButtonWorker)
        {
            _mainMenuViewController = mainMenuViewController;
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
            _adService = adService;
            _backButtonService = backButtonService;
            _backButtonWorker = backButtonWorker;
        }

        public void Enter()
        {
            _adService.CreateAndShowBanner();
            _eventBus.Subscribe<PlayButtonClicked>(OnPlayButtonClicked);
            _mainMenuViewController.Show();
            _backButtonService.PushWorker(_backButtonWorker);
        }

        public void Exit()
        {
            _adService.DestroyBanner();
            _eventBus.Unsubscribe<PlayButtonClicked>(OnPlayButtonClicked);
            _mainMenuViewController.Hide();
            _backButtonService.PopWorker();
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