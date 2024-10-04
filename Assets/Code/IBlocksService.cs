using UnityEngine;

namespace Code
{
    public interface IBlocksService
    {
        void MoveBlocks(Vector2Int direction);
        void ResetBlocksFlags();
    }
}