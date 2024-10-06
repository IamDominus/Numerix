using Code.Infrastructure.Factories;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM;
using Code.Logic;
using Code.Logic.Features;
using Code.Providers;
using Code.Services;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public GameObjectsProvider GameObjectsProvider;
        public InputService InputService;

        public override void InstallBindings()
        {
            Container.Bind<IGameObjectsProvider>().To<GameObjectsProvider>().FromInstance(GameObjectsProvider).AsSingle();
            Container.Bind<IInputService>().To<InputService>().FromInstance(InputService).AsSingle();
            Container.BindInterfacesTo<BlockPositionProvider>().AsSingle();
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.BindInterfacesTo<MoveBlocksService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerTurnService>().AsSingle();
            Container.BindInterfacesTo<BlocksValidationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UndoMoveBlocksService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelDataRepository>().AsSingle();
            Container.Bind<Block>().AsTransient();


            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.BindInterfacesTo<SceneStateRegistrar>().AsSingle().NonLazy();
            Container.Bind<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstructLevelState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();
        }
    }
}