using Zenject;

namespace ZombieApocalypse
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container.Bind<IPlayerInputState>()
            //     .To<PlayerMouseInputState>()
            //     .AsCached();
            Container.BindInterfacesTo<PlayerMouseInputState>().AsSingle();
            Container.BindInterfacesTo<PlayerRotator>().AsSingle();
            Container.BindInterfacesTo<PlayerShootHandler>().AsSingle();
        }
    }
}
