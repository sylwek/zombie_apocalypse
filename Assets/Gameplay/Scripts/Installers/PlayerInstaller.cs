using Zenject;

namespace ZombieApocalypse
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container.Bind<IPlayerRotateState>()
            //     .To<PlayerMouseRotateState>()
            //     .AsCached();
            Container.BindInterfacesTo<PlayerMouseRotateState>().AsCached();

            Container.BindInterfacesTo<PlayerRotator>().AsSingle();
        }
    }
}
