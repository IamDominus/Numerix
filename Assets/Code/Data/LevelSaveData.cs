using System;
using System.Collections.Generic;
using Code.Enums;
using Code.Gameplay;
using Code.Utils;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class LevelSaveData
    {
        public DropOutStack<Block[,]> BlocksStack;
        public DropOutStack<Vector2Int> MoveDirections;
    }
}