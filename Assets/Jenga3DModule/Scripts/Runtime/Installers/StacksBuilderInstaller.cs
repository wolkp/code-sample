using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "StacksBuilderInstaller", menuName = "Installers/StacksBuilderInstaller")]
public class StacksBuilderInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Settings _settings;
   
    public override void InstallBindings()
    {
        Container.BindFactory<StackCreationConfig, Stack, StackFactory>()
            .FromComponentInNewPrefab(_settings.StackPrefab)
            .AsSingle();

        Container.Bind<Settings>().FromInstance(_settings).AsSingle();

        Container.Bind<StacksRegistry>().AsSingle();

        Container.BindInterfacesAndSelfTo<StackFocusController>().AsSingle();

        Container.BindInterfacesTo<StacksBuilder>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public Stack StackPrefab;
        public float SpaceBetweenStacks;
    }
}