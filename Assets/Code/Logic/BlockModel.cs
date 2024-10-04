using Code.Utils;
using UnityEngine;

namespace Code.Logic
{
    public class BlockModel
    {
        public bool MergedThisTurn { get; set; }
        public bool MovedThisTurn { get; set; }
        public long Value { get; set; }
        public Vector2Int Position { get; set; }
        public Vector2Int PreviousPosition1 { get; set; }
        public Vector2Int PreviousPosition2 { get; set; }

        public BlockModel()
        {
            Position = VectorUtils.EMPTY;
            PreviousPosition1 = VectorUtils.EMPTY;
            PreviousPosition2 = VectorUtils.EMPTY;
        }
    }
}