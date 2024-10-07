using Code.Gameplay;
using Code.Gameplay.Views;
using Code.StaticData;

namespace Code.Services
{
    public interface ISpawnService
    {
        public void SpawnCells(LevelStaticData staticData);
        public void SpawnBlock(BlockModel blockModel);
        public void SpawnRandomBlock();
        public bool AbleToSpawnRandomBlock();
        public BlockView SpawnBlockView(BlockModel blockModel);
    }
}