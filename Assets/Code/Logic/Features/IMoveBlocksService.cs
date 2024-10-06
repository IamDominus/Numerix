using UnityEngine;

namespace Code.Logic.Features
{
    public interface IMoveBlocksService
    {
        void MoveBlocks(Vector2Int direction);
        void ResetBlocksFlags();
    }
}