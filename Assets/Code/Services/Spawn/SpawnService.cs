using System.Collections.Generic;
using Code.Gameplay;
using Code.Gameplay.Providers;
using Code.Gameplay.Views;
using Code.Infrastructure.Factories;
using Code.Providers;
using Code.Providers.GameObject;
using Code.Utils;
using UnityEngine;
using Random = System.Random;

namespace Code.Services.Spawn
{
    public class SpawnService : ISpawnService
    {
        private readonly Transform _parent;
        private readonly IDynamicBoundsProvider _dynamicBoundsProvider;
        private readonly ISelectedLevelProvider _selectedLevelProvider;
        private readonly IBlocksProvider _blocksProvider;
        private readonly IGameFactory _gameFactory;
        private readonly Random _random;

        public SpawnService(ILevelObjectsProvider levelObjectsProvider, IDynamicBoundsProvider dynamicBoundsProvider, IGameFactory gameFactory,
            ISelectedLevelProvider selectedLevelProvider, IBlocksProvider blocksProvider)
        {
            _parent = levelObjectsProvider.CellsParent;
            _dynamicBoundsProvider = dynamicBoundsProvider;
            _gameFactory = gameFactory;
            _selectedLevelProvider = selectedLevelProvider;
            _blocksProvider = blocksProvider;
            _random = new Random();
        }

        public void SpawnCells()
        {
            for (var x = 0; x < _selectedLevelProvider.Level.Value.x; x++)
            {
                for (var y = 0; y < _selectedLevelProvider.Level.Value.y; y++)
                {
                    var position = _dynamicBoundsProvider.GetBlockInWorldPosition(x, y);
                    var size = _dynamicBoundsProvider.CellSize;
                    _gameFactory.CreateCell(position, _parent, size);
                }
            }
        }

        public void SpawnBlock(BlockModel blockModel)
        {
            var wordPosition = _dynamicBoundsProvider.GetBlockInWorldPosition(blockModel.Position.x, blockModel.Position.y);
            var block = _gameFactory.CreateBlock(blockModel, wordPosition, _parent, _dynamicBoundsProvider.CellSize, blockModel.Value);
            _blocksProvider.AddBlock(block);
        }

        public void SpawnRandomBlock()
        {
            if (TryGetRandomPosition(_blocksProvider.Blocks, out var position))
            {
                var value = _random.Next(0, 2) == 0 ? 2 : 4;
                var blockModel = new BlockModel(value, position);
                SpawnBlock(blockModel);
            }
        }

        public BlockView SpawnBlockView(BlockModel blockModel)
        {
            var wordPosition = _dynamicBoundsProvider.GetBlockInWorldPosition(blockModel.Position.x, blockModel.Position.y);
            return _gameFactory.CreateBlockView(wordPosition, _parent, _dynamicBoundsProvider.CellSize, blockModel.Value);
        }

        public bool AbleToSpawnRandomBlock()
        {
            return TryGetRandomPosition(_blocksProvider.Blocks, out _);
        }

        private bool TryGetRandomPosition<T>(T[,] array, out Vector2Int position)
        {
            var nullIndexes = new List<Vector2Int>();

            for (var x = 0; x < array.GetLength(0); x++)
            {
                for (var y = 0; y < array.GetLength(1); y++)
                {
                    if (array[x, y] == null)
                    {
                        nullIndexes.Add(new Vector2Int(x, y));
                    }
                }
            }

            if (nullIndexes.Count > 0)
            {
                position = nullIndexes[_random.Next(nullIndexes.Count)];
                return true;
            }

            position = VectorUtils.EMPTY;
            return false;
        }
    }
}