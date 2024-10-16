using Code.Data;

namespace Code.Services.SaveLoad
{
    public interface IGameSaveProvider
    {
        GameSaveData Data { get; set; }
    }
}