using System;
using Code.Data;
using Code.Infrastructure;
using Code.Providers.SaveLoad;
using Code.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Code.Providers
{
    public class SelectedLevelProvider : ISelectedLevelProvider, IInitializable, IDisposable, IGameSaveWriter, IGameSaveReader
    {
        public Observable<Vector2Int> Level { get; set; }

        private readonly ISaveLoadRegistry _saveLoadRegistry;

        public SelectedLevelProvider(ISaveLoadRegistry saveLoadRegistry)
        {
            _saveLoadRegistry = saveLoadRegistry;
            Level = new Observable<Vector2Int>();
        }

        public void Initialize()
        {
            _saveLoadRegistry.RegisterSaveWriter(this);
            _saveLoadRegistry.RegisterSaveReader(this);
        }

        public void SaveGameData(GameSaveData data)
        {
            data.SelectedLevel = Level.Value;
        }

        public void LoadGameData(GameSaveData data)
        {
            Level.Value = data.SelectedLevel;
        }

        public void Dispose()
        {
            _saveLoadRegistry.UnregisterSaveWriter(this);
            _saveLoadRegistry.UnregisterSaveReader(this);
        }
    }
}