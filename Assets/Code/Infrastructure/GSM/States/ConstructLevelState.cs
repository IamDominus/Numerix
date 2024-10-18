using Code.Gameplay.Providers;
using Code.Infrastructure.FSM;
using Code.Providers.SaveLoad;
using Code.Services.Ad;
using Code.Services.Spawn;
using Code.Utils;
using UnityEngine;

namespace Code.Infrastructure.GSM.States
{
    public class ConstructLevelState : IState
    {
        private readonly ISpawnService _spawnService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAdService _adService;
        private readonly IDynamicBoundsProvider _dynamicBoundsProvider;
        private readonly ILevelDataService _levelDataService;
        private readonly IGameSaveProvider _gameSaveProvider;

        public ConstructLevelState(ISpawnService spawnService, GameStateMachine gameStateMachine, IAdService adService,
            IDynamicBoundsProvider dynamicBoundsProvider, ILevelDataService levelDataService, IGameSaveProvider gameSaveProvider)
        {
            _spawnService = spawnService;
            _gameStateMachine = gameStateMachine;
            _adService = adService;
            _dynamicBoundsProvider = dynamicBoundsProvider;
            _levelDataService = levelDataService;
            _gameSaveProvider = gameSaveProvider;
        }

        public void Enter()
        {
            _adService.CreateAndShowBanner();

            _dynamicBoundsProvider.Initialize();
            _spawnService.SpawnCells();

            var levelData = _gameSaveProvider.Data.GetCurrentLevelSaveData();

            if (levelData.IsNotEmpty())
            {
                SpawnBlocksFromSave();
            }
            else
            {
                SpawnRandomBlock();
            }

            _gameStateMachine.Enter<GameplayState>();
        }

        private void SpawnRandomBlock()
        {
            _spawnService.SpawnRandomBlock();
            _levelDataService.PushTurn(Vector2Int.up);
        }

        private void SpawnBlocksFromSave()
        {
            var blockModels = _levelDataService.PeekPreviousTurnBlockModels();
            foreach (var blockModel in blockModels)
            {
                if (blockModel != null)
                {
                    _spawnService.SpawnBlock(blockModel);
                }
            }
        }

        public void Exit()
        {
        }
    }
}