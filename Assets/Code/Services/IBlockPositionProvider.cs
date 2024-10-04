using UnityEngine;

namespace Code.Services
{
    public interface IBlockPositionProvider
    {
        public Vector2 GetBlockInWorldPosition(int x, int y);
        public Vector2 GetBlockInWorldPosition(Vector2Int position);
        bool TryGetRandomPosition<T>(T[,] array, out Vector2Int position);
    }
}