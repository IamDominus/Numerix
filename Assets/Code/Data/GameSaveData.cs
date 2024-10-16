using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class GameSaveData
    {
        public Vector2Int SelectedLevel;
        public List<LevelSaveData> LevelsSaveData;

        public GameSaveData()
        {
            LevelsSaveData = new List<LevelSaveData>();
        }
    }
}