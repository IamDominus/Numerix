using Code.Gameplay.Providers;
using UnityEngine;

namespace Code.Gameplay.Features
{
    public class MoveBlocksFeature : IMoveBlocksFeature
    {
        private Block[,] Blocks => _blocksProvider.Blocks;

        private readonly IBlocksProvider _blocksProvider;
        private readonly IBlocksValidationService _blocksValidationService;

        public MoveBlocksFeature(IBlocksProvider blocksProvider, IBlocksValidationService blocksValidationService)
        {
            _blocksProvider = blocksProvider;
            _blocksValidationService = blocksValidationService;
        }

        public void MoveBlocks(Vector2Int direction)
        {
            var xMax = Blocks.GetLength(0);
            var yMax = Blocks.GetLength(1);

            for (var x = direction.x > 0 ? xMax - 1 : 0; x >= 0 && x < xMax; x += direction.x > 0 ? -1 : 1)
            {
                for (var y = direction.y > 0 ? yMax - 1 : 0; y >= 0 && y < yMax; y += direction.y > 0 ? -1 : 1)
                {
                    var block = Blocks[x, y];
                    if (block == null)
                    {
                        continue;
                    }

                    MoveOrMergeBlock(block, direction.x, direction.y);
                }
            }

            ResetBlocksFlags();
        }

        private void ResetBlocksFlags()
        {
            for (int x = 0; x < Blocks.GetLength(0); x++)
            {
                for (int y = 0; y < Blocks.GetLength(1); y++)
                {
                    var block = Blocks[x, y];
                    if (block != null)
                    {
                        block.ResetFlags();
                    }
                }
            }
        }

        private void MoveOrMergeBlock(Block block, int xDir, int yDir)
        {
            var newX = block.Model.Position.x;
            var newY = block.Model.Position.y;

            while (_blocksValidationService.IsValidPosition(newX + xDir, newY + yDir)
                   && _blocksValidationService.BlockIsEmpty(newX + xDir, newY + yDir))
            {
                newX += xDir;
                newY += yDir;
            }

            if (_blocksValidationService.CanMergeBlocks(block, newX + xDir, newY + yDir))
            {
                MergeBlocks(block, Blocks[newX + xDir, newY + yDir]);
            }
            else
            {
                MoveBlock(block, newX, newY);
            }
        }

        private void MergeBlocks(Block block, Block targetBlock)
        {
            Blocks[targetBlock.Model.Position.x, targetBlock.Model.Position.y] = block;
            Blocks[block.Model.Position.x, block.Model.Position.y] = null;
            targetBlock.DeleteWithDelay();
            block.Merge(targetBlock);
        }

        private void MoveBlock(Block block, int newX, int newY)
        {
            var newPosition = new Vector2Int(newX, newY);
            var oldPosition = block.Model.Position;

            if (newPosition != oldPosition)
            {
                Blocks[newX, newY] = block;
                Blocks[oldPosition.x, oldPosition.y] = null;
            }

            block.Move(newPosition);
        }
    }
}