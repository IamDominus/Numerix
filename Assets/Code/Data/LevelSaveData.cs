using System;
using Code.Gameplay;
using Code.Utils;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class LevelSaveData
    {
        private DropOutStack<BlockModel[,]> BlockModels;
        private DropOutStack<Vector2Int> MoveDirections;

        public LevelSaveData()
        {
            BlockModels = new DropOutStack<BlockModel[,]>(Constants.MAX_UNDO);
            MoveDirections = new DropOutStack<Vector2Int>(Constants.MAX_UNDO);
        }
    }
}