using System;
using Code.Logic;
using Code.Services;
using Code.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code
{
    public class BlockViewService : IInitializable, IDisposable
    {
        private readonly BlockService _blockService;
        private readonly IBlockViewProvider _blockViewProvider;
        private readonly IBlockPositionProvider _blockPositionProvider;
        private readonly ISpawnService _spawnService;
        private BlockView[,] _blockViews;

        public BlockViewService(BlockService blockService, IBlockViewProvider blockViewProvider, IBlockPositionProvider blockPositionProvider, ISpawnService spawnService)
        {
            _blockService = blockService;
            _blockViewProvider = blockViewProvider;
            _blockPositionProvider = blockPositionProvider;
            _spawnService = spawnService;
        }

        public void Initialize()
        {
            _blockViews = _blockViewProvider.Blocks;
            _blockService.BlockMoved += OnBlockMoved;
            _blockService.BlocksMerged += OnBlocksMerged;
            _blockService.BlockGenerated += OnGenerated;
        }

        private void OnBlocksMerged(Vector2Int source, Vector2Int target)
        {
            _blockViews[target.x, target.y].Delete();
            _blockViews[target.x, target.y] = null;
            MoveBlock(source, target, true);
        }

        private void OnBlockMoved(Vector2Int oldPos, Vector2Int newPos)
        {
            MoveBlock(oldPos, newPos, false);
        }

        private void OnGenerated(Block block)
        {
            SpawnBlockAsync(block).Forget();
        }

        private void OnBlocksMoved(Block[,] blocks)
        {
            for (var x = 0; x < blocks.GetLength(0); x++)
            {
                for (var y = 0; y < blocks.GetLength(1); y++)
                {
                    var block = blocks[x, y];

                    if (block == null)
                    {
                        continue;
                    }

                    if (block.MergedThisTurn)
                    {
                        MoveBlock(block.PreviousPosition1, block.Position, block.MergedThisTurn);
                        MoveBlock(block.PreviousPosition2, block.Position, block.MergedThisTurn);
                        SpawnBlockAsync(block).Forget();
                    }
                    else if (block.PreviousPosition1 != block.Position && block.PreviousPosition1 != Vector2IntUtils.EMPTY)
                    {
                        MoveBlock(block.PreviousPosition1, block.Position, block.MergedThisTurn);
                    }
                }
            }
        }

        private async UniTask SpawnBlockAsync(Block block)
        {
            await UniTask.WaitForSeconds(Constants.MOVE_ANIMATION_TIME_SEC);
            _spawnService.SpawnBlock(block.Position, block.Value);
        }

        private void MoveBlock(Vector2Int from, Vector2Int to, bool deleteAfterMove)
        {
            var newPosition = _blockPositionProvider.GetBlockInWorldPosition(to.x, to.y);
            _blockViews[from.x, from.y].MoveTo(newPosition, deleteAfterMove);
            if (deleteAfterMove)
            {
                _blockViews[from.x, from.y] = null;
            }
            else
            {
                _blockViews[to.x, to.y] = _blockViews[from.x, from.y];
                _blockViews[from.x, from.y] = null;
            }
        }

        public void Dispose()
        {
            _blockService.BlockMoved -= OnBlockMoved;
            _blockService.BlocksMerged -= OnBlocksMerged;
            _blockService.BlockGenerated -= OnGenerated;
        }
    }
}