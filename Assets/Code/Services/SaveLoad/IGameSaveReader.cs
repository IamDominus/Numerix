using Code.Data;

namespace Code.Services.SaveLoad
{
    public interface IGameSaveReader
    {
        void LoadGameData(GameSaveData data);
    }
}