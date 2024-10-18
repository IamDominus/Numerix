using Code.Gameplay.Features;
using Code.Gameplay.Providers;
using Code.Services;
using Code.Services.Input;
using Code.Services.SaveLoad;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.Facades
{
    public class UndoMoveBlocksFacade : IUndoMoveBlocksFacade
    {
        private readonly IUndoMoveBlocksFeature _undoMoveBlocksFeature;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IInputService _inputService;
        private readonly ILevelDataService _levelDataService;

        public UndoMoveBlocksFacade(IUndoMoveBlocksFeature undoMoveBlocksFeature, ISaveLoadService saveLoadService, IInputService inputService,
            ILevelDataService levelDataService)
        {
            _undoMoveBlocksFeature = undoMoveBlocksFeature;
            _saveLoadService = saveLoadService;
            _inputService = inputService;
            _levelDataService = levelDataService;
        }

        public async UniTask UndoMoveBlocks()
        {
            if (_levelDataService.TurnHistoryCount() <= 0)
                return;

            _inputService.Disable();

            _undoMoveBlocksFeature.UndoMoveBlocks();
            await UniTask.WaitForSeconds(Constants.MOVE_ANIMATION_TIME_SEC);
            _saveLoadService.SaveGameData();

            _inputService.Enable();
        }
    }
}