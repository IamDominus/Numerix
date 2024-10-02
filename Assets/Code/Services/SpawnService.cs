using Code.Infrastructure.Factories;
using Code.Logic;
using Code.Providers;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Services
{
    public class SpawnService : ISpawnService, IInitializable, IBlockViewProvider
    {
        public BlockView[,] Blocks => _blocks;

        private readonly Transform _parent;
        private readonly IBlockPositionProvider _blockPositionProvider;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IGameFactory _gameFactory;
        private LevelStaticData _staticData;
        private BlockView[,] _blocks;

        public SpawnService(IGameObjectsProvider gameObjectsProvider, IBlockPositionProvider blockPositionProvider, IGameFactory gameFactory,
            IStaticDataProvider staticDataProvider)
        {
            _parent = gameObjectsProvider.CellsParent;
            _blockPositionProvider = blockPositionProvider;
            _gameFactory = gameFactory;
            _staticDataProvider = staticDataProvider;
        }

        public void Initialize()
        {
            _staticData = _staticDataProvider.GetLevelStaticData(Constants.DIMENSIONS);
            _blocks = new BlockView[_staticData.Dimensions.x, _staticData.Dimensions.y];
        }

        public void SpawnCells(LevelStaticData staticData)
        {
            for (var x = 0; x < staticData.Dimensions.x; x++)
            {
                for (var y = 0; y < staticData.Dimensions.y; y++)
                {
                    // Debug.Log($"");
                    var position = _blockPositionProvider.GetBlockInWorldPosition(x, y);
                    _gameFactory.CreateCell(position, _parent, staticData.CellSize);
                }
            }
        }

        public void SpawnBlock(Vector2Int position, long value)
        {
            var wordPosition = _blockPositionProvider.GetBlockInWorldPosition(position.x, position.y);
            _blocks[position.x, position.y] = _gameFactory.CreateBlock(wordPosition, _parent, _staticData.CellSize, value);
        }
    }
}