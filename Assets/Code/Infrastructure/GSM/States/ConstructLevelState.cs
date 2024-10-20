using Code.Gameplay.Providers;
using Code.Infrastructure.FSM;
using Code.Services.Ad;
using Code.Services.BuildLevel;
using Code.Services.Score;
using Code.ViewControllers.HUD;

namespace Code.Infrastructure.GSM.States
{
    public class ConstructLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IDynamicBoundsProvider _dynamicBoundsProvider;
        private readonly IAdService _adService;
        private readonly IScoreService _scoreService;
        private readonly IBuildLevelService _buildLevelService;
        private readonly IHUDViewController _hudViewController;

        public ConstructLevelState(GameStateMachine gameStateMachine, IDynamicBoundsProvider dynamicBoundsProvider, IAdService adService, IScoreService scoreService,
            IBuildLevelService buildLevelService, IHUDViewController hudViewController)
        {
            _gameStateMachine = gameStateMachine;
            _dynamicBoundsProvider = dynamicBoundsProvider;
            _adService = adService;
            _scoreService = scoreService;
            _buildLevelService = buildLevelService;
            _hudViewController = hudViewController;
        }

        public void Enter()
        {
            _adService.CreateBanner();

            _hudViewController.Show();
            _dynamicBoundsProvider.Initialize();

            _buildLevelService.Build();
            _scoreService.UpdateScore();

            _gameStateMachine.Enter<GameplayState>();
        }


        public void Exit()
        {
        }
    }
}