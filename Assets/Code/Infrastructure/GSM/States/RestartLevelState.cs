using Code.Gameplay.Providers;
using Code.Infrastructure.FSM;
using Code.Services.SaveLoad;
using Code.Services.Score;
using Code.Services.Spawn;
using UnityEngine;

namespace Code.Infrastructure.GSM.States
{
    public class RestartLevelState : IState
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IBlocksProvider _blocksProvider;
        private readonly ITurnDataService _turnDataService;
        private readonly IScoreService _scoreService;
        private readonly ISpawnService _spawnService;
        private readonly GameStateMachine _gameStateMachine;

        public RestartLevelState(ISaveLoadService saveLoadService, IBlocksProvider blocksProvider, ITurnDataService turnDataService,
            IScoreService scoreService, ISpawnService spawnService, GameStateMachine gameStateMachine)
        {
            _saveLoadService = saveLoadService;
            _blocksProvider = blocksProvider;
            _turnDataService = turnDataService;
            _scoreService = scoreService;
            _spawnService = spawnService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _blocksProvider.Clear();
            _turnDataService.Clear();

            _spawnService.SpawnRandomBlock();
            _turnDataService.PushTurn(Vector2Int.up);

            _scoreService.UpdateScore();
            _saveLoadService.SaveGameData();

            _gameStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}