using System;
using Code.Enums;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code
{
    public class PlayerTurnService : IInitializable, IDisposable, IPlayerTurnService
    {
        private readonly IInputService _inputService;
        private readonly ISpawnService _spawnService;
        private readonly IBlocksService _blocksService;
        private readonly IBlocksValidationService _blocksValidationService;

        public PlayerTurnService(IInputService inputService, ISpawnService spawnService, IBlocksService blocksService, IBlocksValidationService blocksValidationService)
        {
            _inputService = inputService;
            _spawnService = spawnService;
            _blocksService = blocksService;
            _blocksValidationService = blocksValidationService;
        }

        public void Initialize()
        {
            _inputService.OnDragged += OnDragged;
        }

        public void Start()
        {
            _spawnService.SpawnRandomBlock();
            _inputService.SetEnabled(true);
        }

        private void OnDragged(DragDirection dragDirection)
        {
            PlayTurn(dragDirection).Forget();
        }

        private async UniTask PlayTurn(DragDirection dragDirection)
        {
            var moveDirection = GetMoveDirection(dragDirection);
            if (_blocksValidationService.AbleToMoveBlocks(moveDirection) == false)
            {
                return;
            }

            _inputService.SetEnabled(false);
            _blocksService.MoveBlocks(moveDirection);
            _blocksService.ResetBlocksFlags();

            await UniTask.WaitForSeconds(Constants.DELAY_BEFORE_SPAWN_SEC);

            if (_spawnService.AbleToSpawnRandomBlock())
            {
                _spawnService.SpawnRandomBlock();
                //save game
                _inputService.SetEnabled(true);
            }
            if (_blocksValidationService.AbleToMoveBlocks() == false)
            {
                Debug.Log("GAME IS OVER");
                //TODO move state in state machine(delete save)
                //delete save
            }
        }
        
        //TODO move to different class
        private Vector2Int GetMoveDirection(DragDirection dragDirection)
        {
            switch (dragDirection)
            {
                case DragDirection.Up:
                    return Vector2Int.left;
                case DragDirection.Down:
                    return Vector2Int.right;
                case DragDirection.Left:
                    return Vector2Int.down;
                case DragDirection.Right:
                    return Vector2Int.up;
                default:
                    return Vector2Int.left;
            }
        }

        public void Dispose()
        {
            _inputService.OnDragged -= OnDragged;
            _inputService.SetEnabled(false);
        }
    }
}