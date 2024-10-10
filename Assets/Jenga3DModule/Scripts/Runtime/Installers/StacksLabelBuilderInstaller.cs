using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "StacksLabelBuilderInstaller", menuName = "Installers/StacksLabelBuilderInstaller")]
public class StacksLabelBuilderInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Settings _settings;
   
    public override void InstallBindings()
    {
        Container.BindFactory<StackLabelCreationConfig, StackLabel, StackLabelFactory>()
            .FromComponentInNewPrefab(_settings.StackLabelPrefab)
            .AsSingle();

        Container.Bind<Settings>().FromInstance(_settings).AsSingle();

        Container.BindInterfacesTo<StacksLabelBuilder>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public StackLabel StackLabelPrefab;
        public Vector3 StackLabelOffset;
    }
}