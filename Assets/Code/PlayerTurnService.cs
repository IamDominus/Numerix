using System;
using Code.Enums;
using Code.Logic.Features;
using Code.Services;
using Code.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code
{
    public class PlayerTurnService : IInitializable, IDisposable, IPlayerTurnService
    {
        private readonly IInputService _inputService;
        private readonly ISpawnService _spawnService;
        private readonly IMoveBlocksService _moveBlocksService;
        private readonly IBlocksValidationService _blocksValidationService;
        private readonly IUndoMoveBlocksService _undoMoveBlocksService;
        private readonly ILevelDataRepository _levelDataRepository;

        public PlayerTurnService(IInputService inputService, ISpawnService spawnService, IMoveBlocksService moveBlocksService, IBlocksValidationService blocksValidationService,
            IUndoMoveBlocksService undoMoveBlocksService, ILevelDataRepository levelDataRepository)
        {
            _inputService = inputService;
            _spawnService = spawnService;
            _moveBlocksService = moveBlocksService;
            _blocksValidationService = blocksValidationService;
            _undoMoveBlocksService = undoMoveBlocksService;
            _levelDataRepository = levelDataRepository;
        }

        public void Initialize()
        {
            _inputService.OnDragged += OnDragged;
        }

        public void Start()
        {
            _spawnService.SpawnRandomBlock();
            _levelDataRepository.SaveTurn(Vector2Int.up);
            _inputService.Enable();
        }

        public void Undo()
        {
            _undoMoveBlocksService.UndoTurn().Forget();
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
            _moveBlocksService.MoveBlocks(moveDirection);
            _moveBlocksService.ResetBlocksFlags();
            await UniTask.WaitForSeconds(Constants.DELAY_BEFORE_SPAWN_SEC);

            if (_spawnService.AbleToSpawnRandomBlock())
            {
                _spawnService.SpawnRandomBlock();
                _levelDataRepository.SaveTurn(moveDirection);
                _inputService.Enable();
            }

            if (_blocksValidationService.AbleToMoveBlocks() == false)
            {
                //TODO move state in state machine(delete save)
                //delete save
                Debug.Log("GAME IS OVER");
            }
        }

        public void Dispose()
        {
            _inputService.OnDragged -= OnDragged;
            _inputService.Disable();
        }
    }
}