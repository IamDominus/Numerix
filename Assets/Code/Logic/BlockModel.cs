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

        public BlockModel(long value, Vector2Int position)
        {
            MergedThisTurn = false;
            MovedThisTurn = false;
            Value = value;
            Position = position;
            PreviousPosition1 = VectorUtils.EMPTY;
            PreviousPosition2 = VectorUtils.EMPTY;
        }

        public BlockModel Clone()
        {
            return new BlockModel(this.Value, this.Position)
            {
                MergedThisTurn = this.MergedThisTurn,
                MovedThisTurn = this.MovedThisTurn,
                PreviousPosition1 = this.PreviousPosition1,
                PreviousPosition2 = this.PreviousPosition2
            };
        }
    }
}