using Code.Gameplay.Providers;
using Code.Infrastructure.FSM;
using Code.Services.Ad;
using Code.Services.BuildLevel;
using Code.Services.Score;

namespace Code.Infrastructure.GSM.States
{
    public class ConstructLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IDynamicBoundsProvider _dynamicBoundsProvider;
        private readonly IAdService _adService;
        private readonly IScoreService _scoreService;
        private readonly IBuildLevelService _buildLevelService;

        public ConstructLevelState(GameStateMachine gameStateMachine, IDynamicBoundsProvider dynamicBoundsProvider, IAdService adService, IScoreService scoreService,
            IBuildLevelService buildLevelService)
        {
            _gameStateMachine = gameStateMachine;
            _dynamicBoundsProvider = dynamicBoundsProvider;
            _adService = adService;
            _scoreService = scoreService;
            _buildLevelService = buildLevelService;
        }

        public void Enter()
        {
            _adService.CreateBanner();
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