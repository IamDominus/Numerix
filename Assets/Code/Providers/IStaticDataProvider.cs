using Code.StaticData;
using UnityEngine;

namespace Code.Providers
{
    public interface IStaticDataProvider
    {
        LevelStaticData GetLevelStaticData(Vector2Int dimensions);
    }
}