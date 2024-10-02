using Code.Infrastructure.FSM;
using Code.Services;

namespace Code.Infrastructure.GSM
{
    public class GameplayState : IState
    {
        private readonly IInputService _inputService;
        private readonly BlockService _blockService;

        public GameplayState(IInputService inputService, BlockService blockService)
        {
            _inputService = inputService;
            _blockService = blockService;
        }

        public void Enter()
        {
            _inputService.SetEnabled(true);
            _blockService.Start();
        }

        public void Exit()
        {
        }
    }
}