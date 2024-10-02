using System.Collections.Generic;
using System.Linq;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Providers
{
    public class StaticDataProvider : IInitializable, IStaticDataProvider
    {
        private const string LEVELS_DATA_PATH = "StaticData/Levels";
        private Dictionary<Vector2Int, LevelStaticData> _levelsData;

        public void Initialize()
        {
            _levelsData = Resources
                .LoadAll<LevelStaticData>(LEVELS_DATA_PATH)
                .ToDictionary(x => x.Dimensions, x => x);
        }

        public LevelStaticData GetLevelStaticData(Vector2Int dimensions)
        {
            return _levelsData.GetValueOrDefault(dimensions);
        }
    }
}