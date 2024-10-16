using System.Linq;
using Code.Data;
using Code.Providers;
using Code.Providers.SaveLoad;
using Code.Services.SaveLoad;
using Code.Utils;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Providers
{
    public class LevelDataProvider : IInitializable, ILevelDataProvider, IGameSaveWriter
    {
        private readonly ISelectedLevelProvider _selectedLevelProvider;
        private readonly IGameSaveProvider _gameSaveProvider;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        public Block[,] Blocks { get; private set; }

        private DropOutStack<BlockModel[,]> _blockModels;
        private DropOutStack<Vector2Int> _moveDirections;

        public LevelDataProvider(ISelectedLevelProvider selectedLevelProvider, IGameSaveProvider gameSaveProvider, ISaveLoadRegistry saveLoadRegistry)
        {
            _selectedLevelProvider = selectedLevelProvider;
            _gameSaveProvider = gameSaveProvider;
            _saveLoadRegistry = saveLoadRegistry;
        }

        public void Initialize()
        {
            _saveLoadRegistry.RegisterSaveWriter(this);
            var levelData = _gameSaveProvider.Data.GetOrCreateLevelSaveData(_selectedLevelProvider.Level.Value);

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

            Blocks = new Block[_selectedLevelProvider.Level.Value.x, _selectedLevelProvider.Level.Value.y];
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

        public BlockModel[,] PeekPreviousTurnBlockModels()
        {
            return _blockModels.Peek();
        }

        public Vector2Int PopPreviousTurnMoveDirection()
        {
            return _moveDirections.Pop();
        }

        public void SaveLevelState(Vector2Int moveDirection)
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
            var levelData = data.GetOrCreateLevelSaveData(_selectedLevelProvider.Level.Value);
            levelData.BlockModels = _blockModels.ToList();
            levelData.MoveDirections = _moveDirections.ToList();
        }
    }
}