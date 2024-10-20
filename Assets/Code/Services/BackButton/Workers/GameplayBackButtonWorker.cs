using Code.Enums;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Infrastructure.GSM.States;

namespace Code.Services.BackButton.Workers
{
    public class GameplayBackButtonWorker : IBackButtonWorker
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameplayBackButtonWorker(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void OnBackButtonClicked()
        {
            var payload = new LoadScenePayload()
            {
                SceneName = SceneName.MainMenu,
                Callback = () => _gameStateMachine.Enter<MainMenuState>()
            };
            _gameStateMachine.Enter<LoadSceneState, LoadScenePayload>(payload);
        }
    }
}