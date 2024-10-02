using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public Vector2Int Dimensions;
        public Vector2 CellSize;
        public Vector2 FieldSize;
    }
}