using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BlocksLabelBuilderInstaller", menuName = "Installers/BlocksLabelBuilderInstaller")]
public class BlocksLabelBuilderInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Settings _settings;
   
    public override void InstallBindings()
    {
        Container.BindFactory<BlockLabelCreationConfig, BlockLabel, BlockLabelFactory>()
            .FromComponentInNewPrefab(_settings.BlockLabelPrefab)
            .AsSingle();

        Container.Bind<Settings>().FromInstance(_settings).AsSingle();

        Container.BindInterfacesTo<BlocksLabelBuilder>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public BlockLabel BlockLabelPrefab;
        public Vector3 BlockLabelOffset;
    }
}