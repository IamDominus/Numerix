using Code.EventSystem;
using Code.Infrastructure.Factories;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.States;
using Code.Infrastructure.Runners;
using Code.Providers;
using Code.Providers.SaveLoad;
using Code.Services;
using Code.Services.Ad;
using Code.Services.BackButton;
using Code.Services.BackButton.Workers;
using Code.Services.SaveLoad;
using Code.Views.LoadingCurtain;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            
            Container.BindInterfacesTo<AdService>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<StaticDataProvider>().AsSingle();
            Container.BindInterfacesTo<EventBus>().AsSingle();
            Container.BindInterfacesTo<UIFactory>().AsSingle();
            Container.BindInterfacesTo<BackButtonService>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuBackButtonWorker>().AsSingle();
            
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
                .Bind<ILoadingCurtainView>()
                .To<LoadingCurtainView>()
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