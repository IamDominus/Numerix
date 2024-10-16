using System;
using Code.Enums;
using Code.Providers;
using Code.Services;
using Code.Services.SaveLoad;
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
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly ISaveLoadService _saveLoadService;

        public MoveBlocksFacade(IInputService inputService, ISpawnService spawnService, Features.IMoveBlocksFeature moveBlocksFeature,
            IBlocksValidationService blocksValidationService, ILevelDataProvider levelDataProvider, ISaveLoadService saveLoadService)
        {
            _inputService = inputService;
            _spawnService = spawnService;
            _moveBlocksFeature = moveBlocksFeature;
            _blocksValidationService = blocksValidationService;
            _levelDataProvider = levelDataProvider;
            _saveLoadService = saveLoadService;
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
                _levelDataProvider.SaveLevelState(moveDirection);
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