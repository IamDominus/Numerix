using Code.Data;

namespace Code.Services.SaveLoad
{
    public class GameSaveProvider : IGameSaveProvider
    {
        public GameSaveData Data { get; set; }
    }
}