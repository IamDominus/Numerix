using Cysharp.Threading.Tasks;

namespace Code.Gameplay.Features
{
    public interface IUndoMoveBlocksService
    {
        UniTask UndoTurn();
    }
}