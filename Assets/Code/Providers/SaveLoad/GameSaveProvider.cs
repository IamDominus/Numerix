using Code.Data;

namespace Code.Providers.SaveLoad
{
    public class GameSaveProvider : IGameSaveProvider
    {
        public GameSaveData Data { get; set; }
    }
}