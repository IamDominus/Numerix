using Code.Infrastructure.FSM;
using Code.Providers;
using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.GSM.States
{
    public class ConstructLevelState : IState
    {
        private readonly ISpawnService _spawnService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAddService _addService;
        private readonly IDynamicBoundsProvider _dynamicBoundsProvider;
        private readonly ILevelDataProvider _levelDataProvider;

        public ConstructLevelState(ISpawnService spawnService, GameStateMachine gameStateMachine, IAddService addService,
            IDynamicBoundsProvider dynamicBoundsProvider, ILevelDataProvider levelDataProvider)
        {
            _spawnService = spawnService;
            _gameStateMachine = gameStateMachine;
            _addService = addService;
            _dynamicBoundsProvider = dynamicBoundsProvider;
            _levelDataProvider = levelDataProvider;
        }

        //TODO separate start logic from load saved level logic 
        public void Enter()
        {
            _addService.CreateAndShowBanner();

            _dynamicBoundsProvider.Initialize();
            _spawnService.SpawnCells();

            _spawnService.SpawnRandomBlock();
            _levelDataProvider.SaveLevelState(Vector2Int.up);

            _gameStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}