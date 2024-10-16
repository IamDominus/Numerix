using Code.Gameplay;
using UnityEngine;

namespace Code.Providers
{
    public interface ILevelDataProvider : IBlocksProvider
    {
        void AddBlock(Block block);
        int TurnHistoryCount();
        BlockModel[,] PopPreviousTurnBlockModels();
        Vector2Int PopPreviousTurnMoveDirection();
        void SaveLevelState(Vector2Int moveDirection);
    }
}