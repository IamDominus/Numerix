using System;
using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class LevelSaveData
    {
        public Vector2Int LevelDimensions;
        public List<BlockModel[,]> BlockModels;
        public List<Vector2Int> MoveDirections;

        public LevelSaveData()
        {
            BlockModels = new List<BlockModel[,]>();
            MoveDirections = new List<Vector2Int>();
        }
    }
}