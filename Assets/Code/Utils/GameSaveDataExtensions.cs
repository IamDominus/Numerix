using System.Linq;
using Code.Data;
using UnityEngine;

namespace Code.Utils
{
    public static class GameSaveDataExtensions
    {
        public static LevelSaveData GetOrCreateLevelSaveData(this GameSaveData data, Vector2Int levelDimension)
        {
            var levelData = data.LevelsSaveData.FirstOrDefault(d => d.LevelDimensions == levelDimension);

            if (levelData == null)
            {
                levelData = new LevelSaveData()
                {
                    LevelDimensions = levelDimension
                };
                data.LevelsSaveData.Add(levelData);
            }

            return levelData;
        }

        public static bool IsEmpty(this LevelSaveData data)
        {
            return data.BlockModels.Count == 0 || data.MoveDirections.Count == 0;
        }
    }
}