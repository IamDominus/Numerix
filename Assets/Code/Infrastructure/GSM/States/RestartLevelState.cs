using Code.Gameplay.Providers;
using Code.Infrastructure.FSM;
using Code.Providers.SaveLoad;
using Code.Services.SaveLoad;

namespace Code.Infrastructure.GSM.States
{
    public class RestartLevelState : IState
    {
        private readonly IGameSaveProvider _gameSaveProvider;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IBlocksProvider _blocksProvider;        
        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}