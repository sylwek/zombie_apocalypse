using UnityEngine;
using Zenject;
using ZombieApocalypse;

[CreateAssetMenu(menuName = "Zombie Apocalypse/Project Settings")]
public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameplayDifficultyManager>().AsSingle();
        GameSignalsInstaller.Install(Container);
    }
}