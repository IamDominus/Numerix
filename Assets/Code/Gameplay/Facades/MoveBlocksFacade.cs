using System;
using Code.Enums;
using Code.Gameplay.Providers;
using Code.Services;
using Code.Services.Input;
using Code.Services.SaveLoad;
using Code.Services.Score;
using Code.Services.Spawn;
using Code.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Facades
{
    public class MoveBlocksFacade : IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly ISpawnService _spawnService;
        private readonly Features.IMoveBlocksFeature _moveBlocksFeature;
        private readonly IBlocksValidationService _blocksValidationService;
        private readonly ILevelDataService _levelDataService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IScoreService _scoreService;

        public MoveBlocksFacade(IInputService inputService, ISpawnService spawnService, Features.IMoveBlocksFeature moveBlocksFeature,
            IBlocksValidationService blocksValidationService, ILevelDataService levelDataService, ISaveLoadService saveLoadService, IScoreService scoreService)
        {
            _inputService = inputService;
            _spawnService = spawnService;
            _moveBlocksFeature = moveBlocksFeature;
            _blocksValidationService = blocksValidationService;
            _levelDataService = levelDataService;
            _saveLoadService = saveLoadService;
            _scoreService = scoreService;
        }

        public void Initialize()
        {
            _inputService.OnDragged += OnDragged;
        }

        private void OnDragged(DragDirection dragDirection)
        {
            var moveDirection = VectorUtils.GetMoveDirection(dragDirection);
            if (_blocksValidationService.AbleToMoveBlocks(moveDirection))
            {
                MoveBlocks(moveDirection).Forget();
            }
        }

        private async UniTask MoveBlocks(Vector2Int moveDirection)
        {
            _inputService.Disable();
            _moveBlocksFeature.MoveBlocks(moveDirection);
            await UniTask.WaitForSeconds(Constants.DELAY_BEFORE_SPAWN_SEC);

            if (_spawnService.AbleToSpawnRandomBlock())
            {
                _spawnService.SpawnRandomBlock();
                _scoreService.UpdateScore();
                _levelDataService.PushTurn(moveDirection);
                _inputService.Enable();
            }

            if (_blocksValidationService.AbleToMoveBlocks() == false)
            {
                //TODO move state in state machine(delete save)
                //delete save
                Debug.Log("GAME IS OVER");
            }
            else
            {
                _saveLoadService.SaveGameData();
            }
        }

        public void Dispose()
        {
            _inputService.OnDragged -= OnDragged;
        }
    }
}