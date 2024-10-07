using Code.Gameplay;
using Code.Providers;
using Code.StaticData;
using Code.Utils;
using UnityEngine;
using Zenject;

namespace Code
{
    public class LevelDataRepository : IInitializable, ILevelDataRepository
    {
        public Block[,] Blocks { get; private set; }

        private readonly IStaticDataProvider _staticDataProvider;
        private LevelStaticData _staticData;
        private DropOutStack<BlockModel[,]> _blockModels;
        private DropOutStack<Vector2Int> _moveDirections;

        public LevelDataRepository(IStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
        }

        public void Initialize()
        {
            _staticData = _staticDataProvider.GetLevelStaticData(Constants.DIMENSIONS);
            _blockModels = new DropOutStack<BlockModel[,]>(Constants.MAX_UNDO + 1);
            _moveDirections = new DropOutStack<Vector2Int>(Constants.MAX_UNDO + 1);
            Blocks = new Block[_staticData.Dimensions.x, _staticData.Dimensions.y];
        }

        public void AddBlock(Block block)
        {
            Blocks[block.Model.Position.x, block.Model.Position.y] = block;
        }

        public int TurnHistoryCount()
        {
            return _blockModels.Count() - 1 < 0 ? 0 : _blockModels.Count() - 1;
        }

        public BlockModel[,] PopPreviousTurnBlockModels()
        {
            if (_blockModels.Count() > 1)
            {
                _blockModels.Pop();
                return _blockModels.Peek();
            }

            return _blockModels.Pop();
        }

        public Vector2Int PopPreviousTurnMoveDirection()
        {
            return _moveDirections.Pop();
        }

        public void SaveTurn(Vector2Int moveDirection)
        {
            var blockModels = new BlockModel[_staticData.Dimensions.x, _staticData.Dimensions.y];
            for (int x = 0; x < Blocks.GetLength(0); x++)
            {
                for (int y = 0; y < Blocks.GetLength(1); y++)
                {
                    var block = Blocks[x, y];
                    if (block != null)
                    {
                        blockModels[x, y] = block.Model.Clone();
                    }
                }
            }

            _blockModels.Push(blockModels);
            _moveDirections.Push(moveDirection);

            //TODO call save load service here
        }

        public void Load()
        {
        }
    }
}