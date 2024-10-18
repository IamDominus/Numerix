using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.StateRegistries;
using Code.Infrastructure.GSM.States;
using Code.Providers.GameObject;
using Code.ViewControllers;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public MainMenuObjectsProvider ObjectsProvider;

        public override void InstallBindings()
        {
            BindObjectsProvider();

            BindGameStateMachine();

            BindUI();
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<MainMenuViewController>().AsSingle();
        }

        private void BindObjectsProvider()
        {
            Container.Bind<IMainMenuObjectsProvider>().To<MainMenuObjectsProvider>().FromInstance(ObjectsProvider).AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.BindInterfacesTo<MainMenuStateRegistry>().AsSingle().NonLazy();
            Container.Bind<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
        }
    }
}