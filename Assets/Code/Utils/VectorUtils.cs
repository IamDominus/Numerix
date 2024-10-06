using Code.Data;
using Code.Enums;
using UnityEngine;

namespace Code.Utils
{
    public static class VectorUtils
    {
        public static readonly Vector2Int EMPTY = new Vector2Int(-1, -1);

        public static Vector2Int AsVector2Int(this Vector2IntData vector2IntData)
        {
            return new Vector2Int(vector2IntData.X, vector2IntData.Y);
        }

        public static Vector2IntData AsVector2IntData(this Vector2Int vector2Int)
        {
            return new Vector2IntData(vector2Int.x, vector2Int.y);
        }
        
        public static Vector2Int GetMoveDirection(DragDirection dragDirection)
        {
            switch (dragDirection)
            {
                case DragDirection.Up:
                    return Vector2Int.left;
                case DragDirection.Down:
                    return Vector2Int.right;
                case DragDirection.Left:
                    return Vector2Int.down;
                case DragDirection.Right:
                    return Vector2Int.up;
                default:
                    return Vector2Int.left;
            }
        }
    }
}