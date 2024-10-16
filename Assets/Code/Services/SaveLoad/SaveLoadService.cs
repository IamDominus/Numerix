using Code.Data;
using Code.Utils;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string GAME_DATA_KEY = "GameData";

        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IGameSaveProvider _gameSaveProvider;

        public SaveLoadService(ISaveLoadRegistry saveLoadRegistry, IGameSaveProvider gameSaveProvider)
        {
            _saveLoadRegistry = saveLoadRegistry;
            _gameSaveProvider = gameSaveProvider;
        }

        public void SaveGameData()
        {
            foreach (var gameSaveWriter in _saveLoadRegistry.GameSaveWriters)
            {
                gameSaveWriter.SaveGameData(_gameSaveProvider.Data);
            }

            var json = _gameSaveProvider.Data.ToSerialized();
            PlayerPrefs.SetString(GAME_DATA_KEY, json);
        }

        public GameSaveData LoadGameSave()
        {
            return PlayerPrefs.GetString(GAME_DATA_KEY).ToDeserialized<GameSaveData>();
        }
    }
}