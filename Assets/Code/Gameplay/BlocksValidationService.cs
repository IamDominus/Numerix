using Code.Gameplay.Providers;
using UnityEngine;

namespace Code.Gameplay
{
    public class BlocksValidationService : IBlocksValidationService
    {
        private Block[,] Blocks => _blocksProvider.Blocks;

        private readonly IBlocksProvider _blocksProvider;

        public BlocksValidationService(IBlocksProvider blocksProvider)
        {
            _blocksProvider = blocksProvider;
        }

        public bool AbleToMoveBlocks(Vector2Int direction)
        {
            var xMax = Blocks.GetLength(0);
            var yMax = Blocks.GetLength(1);

            for (var x = direction.x > 0 ? xMax - 1 : 0; x >= 0 && x < xMax; x += direction.x > 0 ? -1 : 1)
            {
                for (var y = direction.y > 0 ? yMax - 1 : 0; y >= 0 && y < yMax; y += direction.y > 0 ? -1 : 1)
                {
                    var block = Blocks[x, y];
                    if (block != null && AbleToMoveBlock(block, direction.x, direction.y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool AbleToMoveBlocks()
        {
            return AbleToMoveBlocks(Vector2Int.up)
                   || AbleToMoveBlocks(Vector2Int.down)
                   || AbleToMoveBlocks(Vector2Int.left)
                   || AbleToMoveBlocks(Vector2Int.right);
        }

        public bool CanMergeBlocks(Block block, int newX, int newY)
        {
            return IsValidPosition(newX, newY)
                   && BlockIsEmpty(newX, newY) == false
                   && block.CanMergeWith(Blocks[newX, newY]);
        }

        public bool BlockIsEmpty(int x, int y)
        {
            return Blocks[x, y] == null;
        }

        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < Blocks.GetLength(0) && y >= 0 && y < Blocks.GetLength(1);
        }

        private bool AbleToMoveBlock(Block block, int xDir, int yDir)
        {
            var newX = block.Model.Position.x;
            var newY = block.Model.Position.y;

            while (IsValidPosition(newX + xDir, newY + yDir) && BlockIsEmpty(newX + xDir, newY + yDir))
            {
                newX += xDir;
                newY += yDir;
            }

            if (CanMergeBlocks(block, newX + xDir, newY + yDir))
            {
                return true;
            }

            if (block.Model.Position.x != newX || block.Model.Position.y != newY)
            {
                return true;
            }

            return false;
        }
    }
}