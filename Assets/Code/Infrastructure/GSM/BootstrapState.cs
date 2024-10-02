using Code.Infrastructure.FSM;

namespace Code.Infrastructure.GSM
{
    public class BootstrapState : IState
    {
        private const string MAIN_SCENE_NAME = "Main";

        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(GameStateMachine gameGameStateMachine)
        {
            _gameStateMachine = gameGameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(MAIN_SCENE_NAME);
        }

        public void Exit()
        {
        }
    }
}