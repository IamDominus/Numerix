using Code.Gameplay.Features;
using Code.Gameplay.Providers;
using Code.Services;
using Code.Services.Input;
using Code.Services.SaveLoad;
using Code.Services.Score;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.Facades
{
    public class UndoMoveBlocksFacade : IUndoMoveBlocksFacade
    {
        private readonly IUndoMoveBlocksFeature _undoMoveBlocksFeature;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IInputService _inputService;
        private readonly ITurnDataService _turnDataService;
        private readonly IScoreService _scoreService;

        public UndoMoveBlocksFacade(IUndoMoveBlocksFeature undoMoveBlocksFeature, ISaveLoadService saveLoadService, IInputService inputService,
            ITurnDataService turnDataService, IScoreService scoreService)
        {
            _undoMoveBlocksFeature = undoMoveBlocksFeature;
            _saveLoadService = saveLoadService;
            _inputService = inputService;
            _turnDataService = turnDataService;
            _scoreService = scoreService;
        }

        public async UniTask UndoMoveBlocks()
        {
            if (_turnDataService.TurnHistoryCount() <= 0)
                return;

            _inputService.Disable();

            _undoMoveBlocksFeature.UndoMoveBlocks();
            _scoreService.UpdateScore();
            _saveLoadService.SaveGameData();
            await UniTask.WaitForSeconds(Constants.MOVE_ANIMATION_TIME_SEC);

            _inputService.Enable();
        }
    }
}