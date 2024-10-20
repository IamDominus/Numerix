using Code.Gameplay.Providers;
using Code.Providers.SaveLoad;
using Code.Services.Spawn;
using Code.Utils;
using UnityEngine;

namespace Code.Services.BuildLevel
{
    public class BuildLevelService : IBuildLevelService
    {
        private readonly ISpawnService _spawnService;
        private readonly IGameSaveProvider _gameSaveProvider;
        private readonly ITurnDataService _turnDataService;

        public BuildLevelService(ISpawnService spawnService, IGameSaveProvider gameSaveProvider, ITurnDataService turnDataService)
        {
            _spawnService = spawnService;
            _gameSaveProvider = gameSaveProvider;
            _turnDataService = turnDataService;
        }

        public void Build()
        {
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
    }
}