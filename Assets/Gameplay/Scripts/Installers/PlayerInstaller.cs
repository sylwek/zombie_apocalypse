using Zenject;

namespace ZombieApocalypse
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerMouseInputState>().AsSingle();
            Container.BindInterfacesTo<PlayerRotator>().AsSingle();
            Container.BindInterfacesTo<PlayerShootHandler>().AsSingle();
        }
    }
}
