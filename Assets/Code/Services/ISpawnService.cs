using Code.StaticData;
using UnityEngine;

namespace Code.Services
{
    public interface ISpawnService
    {
        public void SpawnCells(LevelStaticData staticData);
        public void SpawnBlock(Vector2Int position, long value);
    }
}