using System.Linq;
using Code.Gameplay;
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
            var asd = levelData.BlockModels.Last();
            double val = 2;

            for (int x = 0; x < asd.GetLength(0); x++)
            {
                for (int y = 0; y < asd.GetLength(1); y++)
                {
                    asd[x, y] = new BlockModel(val, new Vector2Int(x, y));
                    if (val < 16777216)
                    {
                        val *= 2;
                    }
                }
            }

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