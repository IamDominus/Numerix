using Code.Enums;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.GSM.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAddService _addService;

        public BootstrapState(GameStateMachine gameGameStateMachine, IAddService addService)
        {
            _gameStateMachine = gameGameStateMachine;
            _addService = addService;
        }

        public void Enter()
        {
            Init().Forget();
        }

        private async UniTask Init()
        {
            await UniTask.WaitUntil(() => _addService.IsInitialized);
            var payload = new LoadScenePayload()
            {
                SceneName = SceneName.MainMenu,
                Callback = () => _gameStateMachine.Enter<MainMenuState>()
            };
            _gameStateMachine.Enter<LoadSceneState, LoadScenePayload>(payload);
        }
        public void Exit()
        {
        }
    }
}