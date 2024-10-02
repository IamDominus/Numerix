using Code.Infrastructure.FSM;
using Code.Providers;
using Code.Services;

namespace Code.Infrastructure.GSM
{
    public class ConstructLevelState : IState
    {
        private readonly ISpawnService _spawnService;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly GameStateMachine _gameStateMachine;

        public ConstructLevelState(ISpawnService spawnService, IStaticDataProvider staticDataProvider, GameStateMachine gameStateMachine)
        {
            _spawnService = spawnService;
            _staticDataProvider = staticDataProvider;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            var staticData = _staticDataProvider.GetLevelStaticData(Constants.DIMENSIONS);
            _spawnService.SpawnCells(staticData);
            _gameStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}