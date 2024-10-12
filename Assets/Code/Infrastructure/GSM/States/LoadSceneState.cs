using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;

namespace Code.Infrastructure.GSM.States
{
    public class LoadSceneState : IPayloadedState<LoadScenePayload>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;

        public LoadSceneState(SceneLoader sceneLoader, ILoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(LoadScenePayload payload)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(payload.SceneName.ToString(), payload.Callback);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }
    }
}