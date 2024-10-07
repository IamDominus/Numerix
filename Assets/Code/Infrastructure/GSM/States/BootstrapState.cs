using Code.Enums;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;

namespace Code.Infrastructure.GSM.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(GameStateMachine gameGameStateMachine)
        {
            _gameStateMachine = gameGameStateMachine;
        }

        public void Enter()
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
        }
    }
}