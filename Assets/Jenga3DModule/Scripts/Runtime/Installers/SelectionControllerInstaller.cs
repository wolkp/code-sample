using UnityEngine;
using UnityEngine.UI;
using Zenject;


[CreateAssetMenu(fileName = "SelectionControllerInstaller", menuName = "Installers/SelectionControllerInstaller")]
public class SelectionControllerInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MouseSelectionController>().FromComponentInHierarchy().AsSingle();

        Container.Bind<GraphicRaycaster>().FromComponentInHierarchy().AsSingle();
    }
}