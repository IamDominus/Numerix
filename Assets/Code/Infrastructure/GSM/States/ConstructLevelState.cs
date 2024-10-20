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
        private readonly IDynamicBoundsProvider _dynamicBoundsProvider;
        private readonly ITurnDataService _turnDataService;
        private readonly IGameSaveProvider _gameSaveProvider;
        private readonly IAdService _adService;

        public ConstructLevelState(ISpawnService spawnService, GameStateMachine gameStateMachine, IDynamicBoundsProvider dynamicBoundsProvider,
            ITurnDataService turnDataService, IGameSaveProvider gameSaveProvider, IAdService adService)
        {
            _spawnService = spawnService;
            _gameStateMachine = gameStateMachine;
            _dynamicBoundsProvider = dynamicBoundsProvider;
            _turnDataService = turnDataService;
            _gameSaveProvider = gameSaveProvider;
            _adService = adService;
        }

        public void Enter()
        {
            _adService.CreateBanner();
            _dynamicBoundsProvider.Initialize();
            _spawnService.SpawnCells();
            InitializeBlocks();

            _gameStateMachine.Enter<GameplayState>();
        }

        private void InitializeBlocks()
        {
            var levelData = _gameSaveProvider.Data.GetCurrentLevelSaveData();

            if (levelData.IsNotEmpty())
            {
                SpawnBlocksFromSave();
            }
            else
            {
                SpawnRandomBlock();
            }
        }

        private void SpawnRandomBlock()
        {
            _spawnService.SpawnRandomBlock();
            _turnDataService.PushTurn(Vector2Int.up);
        }

        private void SpawnBlocksFromSave()
        {
            var blockModels = _turnDataService.PeekPreviousTurnBlockModels();
            foreach (var blockModel in blockModels)
            {
                if (blockModel != null)
                {
                    _spawnService.SpawnBlock(blockModel.Clone());
                }
            }
        }

        public void Exit()
        {
        }
    }
}