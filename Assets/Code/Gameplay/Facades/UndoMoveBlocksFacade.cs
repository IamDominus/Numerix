using Code.Gameplay.Features;
using Code.Providers;
using Code.Services;
using Code.Services.SaveLoad;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.Facades
{
    public class UndoMoveBlocksFacade : IUndoMoveBlocksFacade
    {
        private readonly IUndoMoveBlocksFeature _undoMoveBlocksFeature;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IInputService _inputService;
        private readonly ILevelDataProvider _levelDataProvider;

        public UndoMoveBlocksFacade(IUndoMoveBlocksFeature undoMoveBlocksFeature, ISaveLoadService saveLoadService, IInputService inputService,
            ILevelDataProvider levelDataProvider)
        {
            _undoMoveBlocksFeature = undoMoveBlocksFeature;
            _saveLoadService = saveLoadService;
            _inputService = inputService;
            _levelDataProvider = levelDataProvider;
        }

        public async UniTask UndoMoveBlocks()
        {
            if (_levelDataProvider.TurnHistoryCount() <= 0)
                return;

            _inputService.Disable();

            _undoMoveBlocksFeature.UndoMoveBlocks();
            await UniTask.WaitForSeconds(Constants.MOVE_ANIMATION_TIME_SEC);
            _saveLoadService.SaveGameData();

            _inputService.Enable();
        }
    }
}