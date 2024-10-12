using Code.Gameplay;
using Code.Gameplay.Views;

namespace Code.Services
{
    public interface ISpawnService
    {
        public void SpawnCells();
        public void SpawnBlock(BlockModel blockModel);
        public void SpawnRandomBlock();
        public bool AbleToSpawnRandomBlock();
        public BlockView SpawnBlockView(BlockModel blockModel);
    }
}