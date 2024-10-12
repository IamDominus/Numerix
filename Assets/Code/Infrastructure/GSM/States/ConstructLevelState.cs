using Code.Infrastructure.FSM;
using Code.Providers;
using Code.Services;

namespace Code.Infrastructure.GSM.States
{
    public class ConstructLevelState : IState
    {
        private readonly ISpawnService _spawnService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAddService _addService;
        private readonly IDynamicBoundsProvider _dynamicBoundsProvider;

        public ConstructLevelState(ISpawnService spawnService, GameStateMachine gameStateMachine, IAddService addService,
            IDynamicBoundsProvider dynamicBoundsProvider)
        {
            _spawnService = spawnService;
            _gameStateMachine = gameStateMachine;
            _addService = addService;
            _dynamicBoundsProvider = dynamicBoundsProvider;
        }

        public void Enter()
        {
            _addService.CreateAndShowBanner();
            _dynamicBoundsProvider.Initialize();
            _spawnService.SpawnCells();
            _gameStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}