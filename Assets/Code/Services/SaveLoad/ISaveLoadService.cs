using Code.Data;

namespace Code.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveGameData();
        GameSaveData LoadGameSave();
    }
}