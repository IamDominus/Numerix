using Code.Infrastructure.Factories;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM;
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
            Container.BindInterfacesAndSelfTo<BlockService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BlockViewService>().AsSingle();


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