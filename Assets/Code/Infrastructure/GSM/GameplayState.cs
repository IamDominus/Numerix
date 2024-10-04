using Code.Infrastructure.FSM;

namespace Code.Infrastructure.GSM
{
    public class GameplayState : IState
    {
        private readonly IPlayerTurnService _playerTurnService;

        public GameplayState(IPlayerTurnService playerTurnService)
        {
            _playerTurnService = playerTurnService;
        }

        public void Enter()
        {
            _playerTurnService.Start();
        }

        public void Exit()
        {
        }
    }
}