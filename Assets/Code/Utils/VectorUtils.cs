using Code.Data;
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
    }
}