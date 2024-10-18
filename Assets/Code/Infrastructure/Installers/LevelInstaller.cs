using Code.Gameplay;
using Code.Gameplay.Facades;
using Code.Gameplay.Features;
using Code.Gameplay.Providers;
using Code.Infrastructure.Factories;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.StateRegistries;
using Code.Infrastructure.GSM.States;
using Code.Providers.GameObject;
using Code.Services.HUD;
using Code.Services.Input;
using Code.Services.Score;
using Code.Services.Spawn;
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

            BindBlocks();

            BindGameStateMachine();

            Container.BindInterfacesTo<HUDSafeAreaService>().AsSingle();
            Container.BindInterfacesTo<ScoreService>().AsSingle();
        }

        private void BindObjectsCreationServices()
        {
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindInterfacesTo<GameFactory>().AsSingle();
        }

        private void BindBlocks()
        {
            Container.BindInterfacesTo<LevelDataService>().AsSingle();
            Container.BindInterfacesTo<BlocksProvider>().AsSingle();
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
            Container.BindInterfacesTo<LevelStateRegistry>().AsSingle().NonLazy();
            Container.Bind<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstructLevelState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();
        }
    }
}