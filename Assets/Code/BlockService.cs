using System;
using Code.Enums;
using Code.Logic;
using Code.Providers;
using Code.Services;
using Code.StaticData;
using Code.Utils;
using UnityEngine;
using Zenject;

namespace Code
{
    public class BlockService : IInitializable, IDisposable
    {
        public event Action<Vector2Int, Vector2Int> BlockMoved;
        public event Action<Vector2Int, Vector2Int> BlocksMerged;
        public event Action<Block> BlockGenerated;

        private readonly IInputService _inputService;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IBlockPositionProvider _blockPositionProvider;
        private LevelStaticData _staticData;
        private Block[,] _blocks;

        public BlockService(IInputService inputService, IStaticDataProvider staticDataProvider, IBlockPositionProvider blockPositionProvider)
        {
            _inputService = inputService;
            _staticDataProvider = staticDataProvider;
            _blockPositionProvider = blockPositionProvider;
        }

        public void Initialize()
        {
            _inputService.OnDragged += OnDragged;
            _staticData = _staticDataProvider.GetLevelStaticData(Constants.DIMENSIONS);
            _blocks = new Block[_staticData.Dimensions.x, _staticData.Dimensions.y];
        }

        public void Start()
        {
            SpawnBlock();
        }

        private void OnDragged(DragDirection dragDirection)
        {
            switch (dragDirection)
            {
                case DragDirection.Up:
                    MoveBlocks(-1, 0);
                    break;
                case DragDirection.Down:
                    MoveBlocks(1, 0);
                    break;
                case DragDirection.Left:
                    MoveBlocks(0, -1);
                    break;
                case DragDirection.Right:
                    MoveBlocks(0, 1);
                    break;
            }

            SpawnBlock();
        }

        private bool SpawnBlock()
        {
            if (_blockPositionProvider.TryGetBlockRandomPosition(_blocks, out var position))
            {
                _blocks[position.x, position.y] = new Block()
                {
                    Value = 2,
                    Position = position
                };
                BlockGenerated?.Invoke(_blocks[position.x, position.y]);
                return true;
            }

            return false;
        }

        private void MoveBlocks(int xDir, int yDir)
        {
            var xMax = _staticData.Dimensions.x;
            var yMax = _staticData.Dimensions.y;

            for (var x = xDir > 0 ? xMax - 1 : 0; x >= 0 && x < xMax; x += xDir > 0 ? -1 : 1)
            {
                for (var y = yDir > 0 ? yMax - 1 : 0; y >= 0 && y < yMax; y += yDir > 0 ? -1 : 1)
                {
                    var block = _blocks[x, y];
                    if (block != null)
                    {
                        block.MergedThisTurn = false;
                        block.MovedThisTurn = false;
                        TryMoveBlock(block, xDir, yDir);
                    }
                }
            }
        }

        private void TryMoveBlock(Block block, int xDir, int yDir)
        {
            int newX = block.Position.x;
            int newY = block.Position.y;

            while (IsValidPosition(newX + xDir, newY + yDir) && BlockIsEmpty(newX + xDir, newY + yDir))
            {
                newX += xDir;
                newY += yDir;
            }

            if (CanMergeBlocks(block, newX + xDir, newY + yDir))
            {
                MergeBlocks(block, _blocks[newX + xDir, newY + yDir]);
            }
            else
            {
                MoveBlock(block, newX, newY);
            }
        }

        private void MergeBlocks(Block block, Block targetBlock)
        {
            BlocksMerged?.Invoke(block.Position, targetBlock.Position);

            targetBlock.Value *= 2;
            if (targetBlock.MovedThisTurn == false)
            {
                targetBlock.PreviousPosition1 = targetBlock.Position;
            }

            targetBlock.PreviousPosition2 = block.Position;
            _blocks[block.Position.x, block.Position.y] = null;
            targetBlock.MergedThisTurn = true;
            
            BlockGenerated?.Invoke(targetBlock);
        }

        private void MoveBlock(Block block, int newX, int newY)
        {
            var newPosition = new Vector2Int(newX, newY);
            if (newPosition != block.Position)
            {
                BlockMoved?.Invoke(block.Position, newPosition);
            }

            _blocks[block.Position.x, block.Position.y] = null;
            _blocks[newX, newY] = block;
            block.PreviousPosition1 = block.Position;
            block.PreviousPosition2 = Vector2IntUtils.EMPTY;
            block.Position = newPosition;
            block.MergedThisTurn = false;
            block.MovedThisTurn = true;
        }

        private bool CanMergeBlocks(Block block, int newX, int newY)
        {
            return IsValidPosition(newX, newY) &&
                   BlockIsEmpty(newX, newY) == false &&
                   _blocks[newX, newY].Value == block.Value &&
                   _blocks[newX, newY].MergedThisTurn == false &&
                   block.MergedThisTurn == false;
        }

        private bool BlockIsEmpty(int x, int y)
        {
            return _blocks[x, y] == null;
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < _staticData.Dimensions.x && y >= 0 && y < _staticData.Dimensions.y;
        }

        public void Dispose()
        {
            _inputService.OnDragged -= OnDragged;
        }
    }
}