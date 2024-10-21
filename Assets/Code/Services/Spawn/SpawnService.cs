using System.Collections.Generic;
using Code.Gameplay;
using Code.Gameplay.Providers;
using Code.Gameplay.Views;
using Code.Infrastructure.Factories;
using Code.Providers;
using Code.Utils;
using UnityEngine;
using Random = System.Random;

namespace Code.Services.Spawn
{
    public class SpawnService : ISpawnService
    {
        private readonly IDynamicBoundsProvider _dynamicBoundsProvider;
        private readonly IBlocksProvider _blocksProvider;
        private readonly IGameFactory _gameFactory;
        private readonly Random _random;
        private readonly IRandomBlockValueProvider _blockValueProvider;

        public SpawnService(IDynamicBoundsProvider dynamicBoundsProvider, IGameFactory gameFactory, IBlocksProvider blocksProvider, Random random,
            IRandomBlockValueProvider blockValueProvider)
        {
            _dynamicBoundsProvider = dynamicBoundsProvider;
            _gameFactory = gameFactory;
            _blocksProvider = blocksProvider;
            _random = random;
            _blockValueProvider = blockValueProvider;
        }

        public void SpawnCells()
        {
            for (var x = 0; x < _blocksProvider.Blocks.GetLength(0); x++)
            {
                for (var y = 0; y < _blocksProvider.Blocks.GetLength(1); y++)
                {
                    var position = _dynamicBoundsProvider.GetBlockInWorldPosition(x, y);
                    var size = _dynamicBoundsProvider.CellSize;
                    _gameFactory.CreateCell(position, size);
                }
            }
        }

        public void SpawnBlock(BlockModel blockModel)
        {
            var wordPosition = _dynamicBoundsProvider.GetBlockInWorldPosition(blockModel.Position.x, blockModel.Position.y);
            var block = _gameFactory.CreateBlock(blockModel, wordPosition, _dynamicBoundsProvider.CellSize, blockModel.Value);
            _blocksProvider.AddBlock(block);
        }

        public void SpawnRandomBlock()
        {
            if (TryGetRandomPosition(_blocksProvider.Blocks, out var position))
            {
                var value = _blockValueProvider.GetRandomValue();
                var blockModel = new BlockModel(value, position);
                SpawnBlock(blockModel);
            }
        }

        public BlockView SpawnBlockView(BlockModel blockModel)
        {
            var wordPosition = _dynamicBoundsProvider.GetBlockInWorldPosition(blockModel.Position.x, blockModel.Position.y);
            return _gameFactory.CreateBlockView(wordPosition, _dynamicBoundsProvider.CellSize, blockModel.Value);
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