using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SceneInstaller", menuName = "Installers/SceneInstaller")]
public class SceneInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<StacksParent>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SwitchGradeButtonsParent>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameModeButtonsParent>().FromComponentInHierarchy().AsSingle();
    }
}