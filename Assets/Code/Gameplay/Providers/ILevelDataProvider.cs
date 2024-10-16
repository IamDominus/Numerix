using UnityEngine;

namespace Code.Gameplay.Providers
{
    public interface ILevelDataProvider : IBlocksProvider
    {
        void AddBlock(Block block);
        int TurnHistoryCount();
        BlockModel[,] PopPreviousTurnBlockModels();
        BlockModel[,] PeekPreviousTurnBlockModels();
        Vector2Int PopPreviousTurnMoveDirection();
        void SaveLevelState(Vector2Int moveDirection);
    }
}