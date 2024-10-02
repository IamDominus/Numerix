using Code.Utils;
using UnityEngine;

namespace Code.Logic
{
    public class Block
    {
        public bool MergedThisTurn { get; set; }
        public bool MovedThisTurn { get; set; }
        public long Value { get; set; }
        public Vector2Int Position { get; set; }
        public Vector2Int PreviousPosition1 { get; set; }
        public Vector2Int PreviousPosition2 { get; set; }

        public Block()
        {
            Position = Vector2IntUtils.EMPTY;
            PreviousPosition1 = Vector2IntUtils.EMPTY;
            PreviousPosition2 = Vector2IntUtils.EMPTY;
        }
    }
}