using Code.Infrastructure.FSM;
using Code.Services.Ad;
using Code.Services.Input;

namespace Code.Infrastructure.GSM.States
{
    public class GameplayState : IState
    {
        private readonly IAdService _adService;
        private readonly IInputService _inputService;

        public GameplayState(IAdService adService, IInputService inputService)
        {
            _adService = adService;
            _inputService = inputService;
        }

        public void Enter()
        {
            _adService.CreateAndShowBanner();
            _inputService.Enable();
        }

        public void Exit()
        {
            _inputService.Disable();
            _adService.DestroyBanner();
        }
    }
}