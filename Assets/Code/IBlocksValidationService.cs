using Code.Logic;
using UnityEngine;

namespace Code
{
    public interface IBlocksValidationService
    {
        bool AbleToMoveBlocks();
        bool AbleToMoveBlocks(Vector2Int direction);
        bool CanMergeBlocks(Block block, int newX, int newY);
        bool BlockIsEmpty(int x, int y);
        bool IsValidPosition(int x, int y);
    }
}