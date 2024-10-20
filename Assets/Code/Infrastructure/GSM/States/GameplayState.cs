using Code.Enums;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Services.Ad;
using Code.Services.BackButton;
using Code.Services.BackButton.Workers;
using Code.Services.Input;

namespace Code.Infrastructure.GSM.States
{
    public class GameplayState : IState
    {
        private readonly IAdService _adService;
        private readonly IInputService _inputService;
        private readonly IBackButtonService _backButtonService;
        private readonly IBackButtonWorker _backButtonWorker;

        public GameplayState(IAdService adService, IInputService inputService, IBackButtonService backButtonService, GameplayBackButtonWorker backButtonWorker)
        {
            _adService = adService;
            _inputService = inputService;
            _backButtonService = backButtonService;
            _backButtonWorker = backButtonWorker;
        }

        public void Enter()
        {
            _adService.ShowBanner();
            _inputService.Enable();
            _backButtonService.PushWorker(_backButtonWorker);
        }

        public void Exit()
        {
            _inputService.Disable();
            _adService.HideBanner();
            _backButtonService.PopWorker();
        }
    }
}