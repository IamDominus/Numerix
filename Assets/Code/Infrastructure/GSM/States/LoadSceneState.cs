using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Views.LoadingCurtain;

namespace Code.Infrastructure.GSM.States
{
    public class LoadSceneState : IPayloadedState<LoadScenePayload>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ILoadingCurtainView _loadingCurtainView;

        public LoadSceneState(SceneLoader sceneLoader, ILoadingCurtainView loadingCurtainView)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtainView = loadingCurtainView;
        }

        public void Enter(LoadScenePayload payload)
        {
            _loadingCurtainView.Show();
            _sceneLoader.Load(payload.SceneName.ToString(), payload.Callback);
        }

        public void Exit()
        {
            _loadingCurtainView.Hide();
        }
    }
}