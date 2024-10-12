using UnityEngine;

namespace Code.Providers
{
    public interface IDynamicBoundsProvider
    {
        Vector2 FieldSize { get; }
        Vector2 FieldPosition { get; }
        Vector2 CellSize { get; }
        void Initialize();
        public Vector2 GetBlockInWorldPosition(int x, int y);
        public Vector2 GetBlockInWorldPosition(Vector2Int position);
    }
}