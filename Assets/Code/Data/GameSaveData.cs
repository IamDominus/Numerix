using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class GameSaveData
    {
        public Vector2Int SelectedLevel;
        public Dictionary<Vector2Int, LevelSaveData> LevelsSaveData;

        public GameSaveData()
        {
            LevelsSaveData = new Dictionary<Vector2Int, LevelSaveData>();

            for (var x = 0; x < Constants.MAX_DIMENSIONS.x; x++)
            {
                for (var y = 0; y < Constants.MAX_DIMENSIONS.y; y++)
                {
                    var key = new Vector2Int(x, y);
                    LevelsSaveData.Add(key, new LevelSaveData());
                }
            }
        }
    }
}