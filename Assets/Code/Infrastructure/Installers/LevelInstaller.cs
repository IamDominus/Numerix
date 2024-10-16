using Code.Gameplay;
using Code.Gameplay.Facades;
using Code.Gameplay.Features;
using Code.Infrastructure.Factories;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.StateRegistrars;
using Code.Infrastructure.GSM.States;
using Code.Providers;
using Code.Providers.GameObject;
using Code.Services;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public LevelObjectsProvider LevelObjectsProvider;
        public InputService InputService;

        public override void InstallBindings()
        {
            BindObjectsProvider();

            BindInput();

            BindObjectsCreationServices();

            BindGameplayFeatures();

            BindHelpers();

            BindDataServices();

            BindControllers();

            BindGameStateMachine();

            Container.BindInterfacesTo<HUDSafeAreaService>().AsSingle();
        }

        private void BindObjectsCreationServices()
        {
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindInterfacesTo<GameFactory>().AsSingle();
        }

        private void BindDataServices()
        {
            Container.BindInterfacesAndSelfTo<LevelDataProvider>().AsSingle();
        }

        private void BindControllers()
        {
            Container.Bind<Block>().AsTransient();
        }

        private void BindHelpers()
        {
            Container.BindInterfacesAndSelfTo<DynamicBoundsProvider>().AsSingle();
            Container.BindInterfacesTo<BlocksValidationService>().AsSingle();
        }

        private void BindGameplayFeatures()
        {
            Container.BindInterfacesTo<MoveBlocksFacade>().AsSingle();
            Container.BindInterfacesTo<UndoMoveBlocksFacade>().AsSingle();
            
            Container.BindInterfacesTo<MoveBlocksFeature>().AsSingle();
            Container.BindInterfacesTo<UndoMoveBlocksFeature>().AsSingle();
        }

        private void BindInput()
        {
            Container.Bind<IInputService>().To<InputService>().FromInstance(InputService).AsSingle();
        }

        private void BindObjectsProvider()
        {
            Container.Bind<ILevelObjectsProvider>().To<LevelObjectsProvider>().FromInstance(LevelObjectsProvider).AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.BindInterfacesTo<LevelStateRegistrar>().AsSingle().NonLazy();
            Container.Bind<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstructLevelState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();
        }
    }
}