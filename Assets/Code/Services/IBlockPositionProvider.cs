using Code.Logic;
using UnityEngine;

namespace Code.Services
{
    public interface IBlockPositionProvider
    {
        public Vector2 GetBlockInWorldPosition(int y, int x);
        bool TryGetBlockRandomPosition(Block[,] blocks, out Vector2Int position);
    }
}