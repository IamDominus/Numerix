using Code.EventSystem;
using Code.Infrastructure.Factories;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.States;
using Code.Infrastructure.Runners;
using Code.Providers;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            
            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<StaticDataProvider>().AsSingle();
            Container.BindInterfacesTo<EventBus>().AsSingle();
            Container.BindInterfacesTo<UIFactory>().AsSingle();

            BindGameStateMachine();
        }

        private void BindCoroutineRunner()
        {
            Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromComponentInNewPrefabResource(Constants.Resouces.COROUTINE_RUNNER).AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<StateFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsCached();
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadSceneState>().AsSingle();
        }
    }
}