using System;
using Code.Data;
using Code.Providers.SaveLoad;
using Code.Services.SaveLoad;
using Code.Utils;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Providers
{
    public class TurnDataService : IInitializable, ITurnDataService, IGameSaveWriter, IDisposable
    {
        private Block[,] Blocks => _blocksProvider.Blocks;

        private readonly IGameSaveProvider _gameSaveProvider;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IBlocksProvider _blocksProvider;

        private DropOutStack<BlockModel[,]> _blockModels;
        private DropOutStack<Vector2Int> _moveDirections;

        public TurnDataService(IGameSaveProvider gameSaveProvider, ISaveLoadRegistry saveLoadRegistry, IBlocksProvider blocksProvider)
        {
            _gameSaveProvider = gameSaveProvider;
            _saveLoadRegistry = saveLoadRegistry;
            _blocksProvider = blocksProvider;
        }

        public void Initialize()
        {
            _saveLoadRegistry.RegisterSaveWriter(this);
            var levelData = _gameSaveProvider.Data.GetCurrentLevelSaveData();

            if (levelData.IsEmpty())
            {
                _blockModels = new DropOutStack<BlockModel[,]>(Constants.MAX_UNDO + 1);
                _moveDirections = new DropOutStack<Vector2Int>(Constants.MAX_UNDO + 1);
            }
            else
            {
                _blockModels = new DropOutStack<BlockModel[,]>(Constants.MAX_UNDO + 1, levelData.BlockModels);
                _moveDirections = new DropOutStack<Vector2Int>(Constants.MAX_UNDO + 1, levelData.MoveDirections);
            }
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

        public BlockModel[,] PeekPreviousTurnBlockModels()
        {
            return _blockModels.Peek();
        }

        public Vector2Int PopPreviousTurnMoveDirection()
        {
            return _moveDirections.Pop();
        }

        public void PushTurn(Vector2Int moveDirection)
        {
            var blockModels = new BlockModel[Blocks.GetLength(0), Blocks.GetLength(1)];

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
        }

        public void SaveGameData(GameSaveData data)
        {
            var levelData = data.GetCurrentLevelSaveData();
            levelData.BlockModels = _blockModels.ToList();
            levelData.MoveDirections = _moveDirections.ToList();
        }

        public void Clear()
        {
            _blockModels.Clear();
            _moveDirections.Clear();
        }

        public void Dispose()
        {
            _saveLoadRegistry.UnregisterSaveWriter(this);
        }
    }
}