﻿using System;
using Code.Enums;
using Code.Gameplay.Providers;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.States;
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
        private readonly ITurnDataService _turnDataService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IScoreService _scoreService;
        private readonly GameStateMachine _gameStateMachine;

        public MoveBlocksFacade(IInputService inputService, ISpawnService spawnService, Features.IMoveBlocksFeature moveBlocksFeature,
            IBlocksValidationService blocksValidationService, ITurnDataService turnDataService, ISaveLoadService saveLoadService, IScoreService scoreService,
            GameStateMachine gameStateMachine)
        {
            _inputService = inputService;
            _spawnService = spawnService;
            _moveBlocksFeature = moveBlocksFeature;
            _blocksValidationService = blocksValidationService;
            _turnDataService = turnDataService;
            _saveLoadService = saveLoadService;
            _scoreService = scoreService;
            _gameStateMachine = gameStateMachine;
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

            _spawnService.SpawnRandomBlock();
            _scoreService.UpdateScore();
            _turnDataService.PushTurn(moveDirection);
            _saveLoadService.SaveGameData();
            _inputService.Enable();

            if (_blocksValidationService.AbleToMoveBlocks() == false)
            {
                _gameStateMachine.Enter<GameOverState>();
            }
        }

        public void Dispose()
        {
            _inputService.OnDragged -= OnDragged;
        }
    }
}