using Code.EventSystem;
using Code.Infrastructure.Factories;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.States;
using Code.Infrastructure.Runners;
using Code.Providers;
using Code.Services.SaveLoad;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            
            Container.BindInterfacesTo<AddService>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<StaticDataProvider>().AsSingle();
            Container.BindInterfacesTo<EventBus>().AsSingle();
            Container.BindInterfacesTo<UIFactory>().AsSingle();
            
            BindSaveLoad();

            BindLoadingCurtain();

            BindGameStateMachine();
        }

        private void BindSaveLoad()
        {
            Container.BindInterfacesTo<SelectedLevelProvider>().AsSingle();
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
            Container.BindInterfacesTo<SaveLoadRegistry>().AsSingle();
            Container.BindInterfacesTo<GameSaveProvider>().AsSingle();
        }

        private void BindLoadingCurtain()
        {
            Container
                .Bind<ILoadingCurtain>()
                .To<LoadingCurtain>()
                .FromComponentInNewPrefabResource(Constants.Resouces.LOADING_CURTAIN)
                .AsSingle();
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
            Container.BindInterfacesAndSelfTo<LoadGameState>().AsSingle();
        }
    }
}