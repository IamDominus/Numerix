using Code.Infrastructure.FSM;

namespace Code.Infrastructure.GSM.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ILoadingCurtain _loadingCurtain;

        public BootstrapState(GameStateMachine gameGameStateMachine, ILoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameGameStateMachine;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _gameStateMachine.Enter<LoadGameState>();
        }

        public void Exit()
        {
        }
    }
}