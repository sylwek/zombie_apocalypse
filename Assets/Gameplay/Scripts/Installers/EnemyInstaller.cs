using Zenject;

namespace ZombieApocalypse
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EnemyMover>().AsSingle();
        }
    }
}
