using Code.EventSystem;
using Code.Infrastructure.AssetLoading;
using Code.Infrastructure.Factories;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.States;
using Code.Infrastructure.Runners;
using Code.Providers;
using Code.Providers.SaveLoad;
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

            BindAd();

            BindSceneLoader();

            BindStaticData();

            BindEvents();

            BindUI();

            BindBackButton();

            BindSaveLoad();

            BindLoadingCurtain();

            BindGameStateMachine();

            BindAssetLoader();
        }

        private void BindAssetLoader()
        {
            Container.BindInterfacesTo<AssetLoader>().AsSingle();
        }

        private void BindStaticData()
        {
            Container.BindInterfacesTo<StaticDataProvider>().AsSingle();
        }

        private void BindEvents()
        {
            Container.BindInterfacesTo<EventBus>().AsSingle();
        }

        private void BindUI()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }

        private void BindBackButton()
        {
            Container.BindInterfacesTo<BackButtonService>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuBackButtonWorker>().AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.Bind<SceneLoader>().AsSingle();
        }

        private void BindAd()
        {
            Container.BindInterfacesTo<AdService>().AsSingle().NonLazy();
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