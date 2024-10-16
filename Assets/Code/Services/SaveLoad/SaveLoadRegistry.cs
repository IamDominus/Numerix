using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public class SaveLoadRegistry : ISaveLoadRegistry
    {
        public List<IGameSaveWriter> GameSaveWriters { get; set; }
        public List<IGameSaveReader> GameSaveReaders { get; set; }

        public SaveLoadRegistry()
        {
            GameSaveWriters = new List<IGameSaveWriter>();
            GameSaveReaders = new List<IGameSaveReader>();
        }

        public void RegisterSaveWriter(IGameSaveWriter gameSaveWriter)
        {
            if (GameSaveWriters.Contains(gameSaveWriter) == false)
            {
                GameSaveWriters.Add(gameSaveWriter);
            }
            else
            {
                Debug.Log($"Game save writer {gameSaveWriter.GetType().Name} already registered");
            }
        }

        public void UnregisterSaveWriter(IGameSaveWriter gameSaveWriter)
        {
            GameSaveWriters.Remove(gameSaveWriter);
        }

        public void RegisterSaveReader(IGameSaveReader gameSaveReader)
        {
            if (GameSaveReaders.Contains(gameSaveReader) == false)
            {
                GameSaveReaders.Add(gameSaveReader);
            }
            else
            {
                Debug.Log($"Game save reader {gameSaveReader.GetType().Name} already registered");
            }
        }

        public void UnregisterSaveReader(IGameSaveReader gameSaveReader)
        {
            GameSaveReaders.Remove(gameSaveReader);
        }

        public void Clear()
        {
            GameSaveWriters.Clear();
            GameSaveReaders.Clear();
        }
    }
}