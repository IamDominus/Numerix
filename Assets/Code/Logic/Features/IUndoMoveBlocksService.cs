using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Features
{
    public interface IUndoMoveBlocksService
    {
        UniTask UndoTurn();
    }
}