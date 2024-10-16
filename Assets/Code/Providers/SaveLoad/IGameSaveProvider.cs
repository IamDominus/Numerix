using Code.Data;

namespace Code.Providers.SaveLoad
{
    public interface IGameSaveProvider
    {
        GameSaveData Data { get; set; }
    }
}