using Cysharp.Threading.Tasks;

namespace Code.Gameplay.Facades
{
    public interface IUndoMoveBlocksFacade
    {
        UniTask UndoMoveBlocks();
    }
}