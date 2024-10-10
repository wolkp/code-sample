using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[CreateAssetMenu(fileName = "BlockDetailsPanelInstaller", menuName = "Installers/BlockDetailsPanelInstaller")]
public class BlockDetailsPanelInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Settings _settings;
   
    public override void InstallBindings()
    {
        Container.Bind<Settings>().FromInstance(_settings).AsSingle();

        Container.Bind<CanvasScaler>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesTo<BlockDetailsPanel>().FromComponentInHierarchy().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public Vector2 PanelOffset;
        public Vector2 ScreenMargins;
    }
}