using Code.Data;

namespace Code.Services.SaveLoad
{
    public interface IGameSaveWriter 
    {
        void SaveGameData(GameSaveData data);
    }
}