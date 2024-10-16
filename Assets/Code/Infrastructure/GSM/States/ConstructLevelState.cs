using Code.Data;
using Code.Gameplay.Providers;
using Code.Infrastructure.FSM;
using Code.Providers;
using Code.Providers.SaveLoad;
using Code.Services;
using Code.Services.Ad;
using Code.Services.SaveLoad;
using Code.Services.Spawn;
using Code.Utils;
using UnityEngine;

namespace Code.Infrastructure.GSM.States
{
    public class ConstructLevelState : IState
    {
        private readonly ISpawnService _spawnService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAdService _adService;
        private readonly IDynamicBoundsProvider _dynamicBoundsProvider;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IGameSaveProvider _gameSaveProvider;
        private readonly ISelectedLevelProvider _selectedLevelProvider;

        public ConstructLevelState(ISpawnService spawnService, GameStateMachine gameStateMachine, IAdService adService,
            IDynamicBoundsProvider dynamicBoundsProvider, ILevelDataProvider levelDataProvider, IGameSaveProvider gameSaveProvider, ISelectedLevelProvider selectedLevelProvider)
        {
            _spawnService = spawnService;
            _gameStateMachine = gameStateMachine;
            _adService = adService;
            _dynamicBoundsProvider = dynamicBoundsProvider;
            _levelDataProvider = levelDataProvider;
            _gameSaveProvider = gameSaveProvider;
            _selectedLevelProvider = selectedLevelProvider;
        }

        //TODO separate start logic from load saved level logic 
        public void Enter()
        {
            _adService.CreateAndShowBanner();

            _dynamicBoundsProvider.Initialize();
            _spawnService.SpawnCells();

            var levelData = _gameSaveProvider.Data.GetOrCreateLevelSaveData(_selectedLevelProvider.Level.Value);
            if (levelData.IsEmpty() == false)
            {
                var blockModels = _levelDataProvider.PeekPreviousTurnBlockModels();
                foreach (var blockModel in blockModels)
                {
                    if (blockModel != null)
                    {
                        _spawnService.SpawnBlock(blockModel);
                    }
                }
            }
            else
            {
                _spawnService.SpawnRandomBlock();
                _levelDataProvider.SaveLevelState(Vector2Int.up);
            }


            _gameStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}