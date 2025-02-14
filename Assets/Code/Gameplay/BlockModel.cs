﻿using System;
using Code.Utils;
using UnityEngine;

namespace Code.Gameplay
{
    [Serializable]
    public class BlockModel
    {
        public bool MergedThisTurn { get; set; }
        public bool MovedThisTurn { get; set; }
        public double Value { get; set; }
        public Vector2Int Position { get; set; }
        public Vector2Int PreviousPosition1 { get; set; }
        public Vector2Int PreviousPosition2 { get; set; }

        public BlockModel(double value, Vector2Int position)
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