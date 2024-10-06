using Code.Infrastructure.Factories;
using Code.Logic;
using Code.Providers;
using Code.StaticData;
using Code.Views;
using UnityEngine;
using Zenject;

namespace Code.Services
{
    public class SpawnService : ISpawnService, IInitializable
    {
        private readonly Transform _parent;
        private readonly IBlockPositionProvider _blockPositionProvider;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ILevelDataRepository _levelDataRepository;
        private readonly IGameFactory _gameFactory;
        private LevelStaticData _staticData;

        public SpawnService(IGameObjectsProvider gameObjectsProvider, IBlockPositionProvider blockPositionProvider, IGameFactory gameFactory,
            IStaticDataProvider staticDataProvider, ILevelDataRepository levelDataRepository)
        {
            _parent = gameObjectsProvider.CellsParent;
            _blockPositionProvider = blockPositionProvider;
            _gameFactory = gameFactory;
            _staticDataProvider = staticDataProvider;
            _levelDataRepository = levelDataRepository;
        }

        public void Initialize()
        {
            _staticData = _staticDataProvider.GetLevelStaticData(Constants.DIMENSIONS);
        }

        public void SpawnCells(LevelStaticData staticData)
        {
            for (var x = 0; x < staticData.Dimensions.x; x++)
            {
                for (var y = 0; y < staticData.Dimensions.y; y++)
                {
                    var position = _blockPositionProvider.GetBlockInWorldPosition(x, y);
                    _gameFactory.CreateCell(position, _parent, staticData.CellSize);
                }
            }
        }

        public void SpawnBlock(BlockModel blockModel)
        {
            var wordPosition = _blockPositionProvider.GetBlockInWorldPosition(blockModel.Position.x, blockModel.Position.y);
            var block = _gameFactory.CreateBlock(blockModel, wordPosition, _parent, _staticData.CellSize, blockModel.Value);
            _levelDataRepository.AddBlock(block);
        }

        public void SpawnRandomBlock()
        {
            if (_blockPositionProvider.TryGetRandomPosition(_levelDataRepository.Blocks, out var position))
            {
                var blockModel = new BlockModel(2, position);
                SpawnBlock(blockModel);
            }
        }

        public BlockView SpawnBlockView(BlockModel blockModel)
        {
            var wordPosition = _blockPositionProvider.GetBlockInWorldPosition(blockModel.Position.x, blockModel.Position.y);
            return _gameFactory.CreateBlockView(wordPosition, _parent, _staticData.CellSize, blockModel.Value);
        }

        public bool AbleToSpawnRandomBlock()
        {
            return _blockPositionProvider.TryGetRandomPosition(_levelDataRepository.Blocks, out _);
        }
    }
}