using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;

namespace Code.Infrastructure.GSM.States
{
    public class LoadSceneState : IPayloadedState<LoadScenePayload>
    {
        private readonly SceneLoader _sceneLoader;

        public LoadSceneState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(LoadScenePayload payload)
        {
            _sceneLoader.Load(payload.SceneName.ToString(), payload.Callback);
        }

        public void Exit()
        {
        }
    }
}