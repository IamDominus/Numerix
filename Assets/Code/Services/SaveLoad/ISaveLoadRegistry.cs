using System.Collections.Generic;

namespace Code.Services.SaveLoad
{
    public interface ISaveLoadRegistry
    {
        List<IGameSaveWriter> GameSaveWriters { get; set; }
        List<IGameSaveReader> GameSaveReaders { get; set; }
        void RegisterSaveWriter(IGameSaveWriter gameSaveWriter);
        void UnregisterSaveWriter(IGameSaveWriter gameSaveWriter);
        void RegisterSaveReader(IGameSaveReader gameSaveReader);
        void UnregisterSaveReader(IGameSaveReader gameSaveReader);
        void Clear();
    }
}