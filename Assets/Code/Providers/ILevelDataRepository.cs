using Code.Gameplay;
using UnityEngine;

namespace Code.Providers
{
    public interface ILevelDataRepository : IBlocksProvider
    {
        void AddBlock(Block block);
        int TurnHistoryCount();
        BlockModel[,] PopPreviousTurnBlockModels();
        Vector2Int PopPreviousTurnMoveDirection();
        void SaveTurn(Vector2Int moveDirection);
    }
}