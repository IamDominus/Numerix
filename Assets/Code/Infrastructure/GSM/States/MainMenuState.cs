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

        public MainMenuState(MainMenuViewController mainMenuViewController, IEventBus eventBus, GameStateMachine gameStateMachine)
        {
            _mainMenuViewController = mainMenuViewController;
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _eventBus.Subscribe<PlayButtonClicked>(OnPlayButtonClicked);
            _mainMenuViewController.Show();
        }

        public void Exit()
        {
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