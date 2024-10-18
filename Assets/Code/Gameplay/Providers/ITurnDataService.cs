using UnityEngine;

namespace Code.Gameplay.Providers
{
    public interface ITurnDataService
    {
        int TurnHistoryCount();
        BlockModel[,] PopPreviousTurnBlockModels();
        BlockModel[,] PeekPreviousTurnBlockModels();
        Vector2Int PopPreviousTurnMoveDirection();
        void PushTurn(Vector2Int moveDirection);
        void Clear();
    }
}