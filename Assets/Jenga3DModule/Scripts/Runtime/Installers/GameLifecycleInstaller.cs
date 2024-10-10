using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameLifecycleInstaller", menuName = "Installers/GameLifecycleInstaller")]
public class GameLifecycleInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameLifecycleController>().AsSingle();
    }
}