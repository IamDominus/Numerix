using Code.Data;
using Code.Enums;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Providers.SaveLoad;
using Code.Services;
using Code.Services.Ad;
using Code.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.GSM.States
{
    public class LoadGameState : IState
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameSaveProvider _gameSaveProvider;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAdService _adService;
        private readonly ISaveLoadRegistry _saveLoadRegistry;

        public LoadGameState(ISaveLoadService saveLoadService, IGameSaveProvider gameSaveProvider, GameStateMachine gameStateMachine, IAdService adService,
            ISaveLoadRegistry saveLoadRegistry)
        {
            _saveLoadService = saveLoadService;
            _gameSaveProvider = gameSaveProvider;
            _gameStateMachine = gameStateMachine;
            _adService = adService;
            _saveLoadRegistry = saveLoadRegistry;
        }

        public void Enter()
        {
            InitializeGameAsync().Forget();
        }

        public void Exit()
        {
        }

        private async UniTask InitializeGameAsync()
        {
            LoadSaveAndNotifySubs();

            await UniTask.WaitUntil(() => _adService.IsInitialized);

            EnterMainMenu();
        }

        private void LoadSaveAndNotifySubs()
        {
            _gameSaveProvider.Data = _saveLoadService.LoadGameSave() ?? CreateInitialGameSave();
            foreach (var gameSaveReader in _saveLoadRegistry.GameSaveReaders)
            {
                gameSaveReader.LoadGameData(_gameSaveProvider.Data);
            }
        }

        private void EnterMainMenu()
        {
            var payload = new LoadScenePayload()
            {
                SceneName = SceneName.MainMenu,
                Callback = () => _gameStateMachine.Enter<MainMenuState>()
            };
            _gameStateMachine.Enter<LoadSceneState, LoadScenePayload>(payload);
        }

        private GameSaveData CreateInitialGameSave()
        {
            return new GameSaveData()
            {
                SelectedLevel = new Vector2Int(4, 4)
            };
        }
    }
}