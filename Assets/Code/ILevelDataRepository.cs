using Code.Gameplay;
using Code.Providers;
using UnityEngine;

namespace Code
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