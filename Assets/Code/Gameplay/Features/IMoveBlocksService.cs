using UnityEngine;

namespace Code.Gameplay.Features
{
    public interface IMoveBlocksService
    {
        void MoveBlocks(Vector2Int direction);
        void ResetBlocksFlags();
    }
}