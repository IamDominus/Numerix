using UnityEngine;

namespace Code.Gameplay.Providers
{
    public interface ILevelDataService
    {
        int TurnHistoryCount();
        BlockModel[,] PopPreviousTurnBlockModels();
        BlockModel[,] PeekPreviousTurnBlockModels();
        Vector2Int PopPreviousTurnMoveDirection();
        void PushTurn(Vector2Int moveDirection);
        void Clear();
    }
}