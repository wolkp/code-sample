using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BlocksBuilderInstaller", menuName = "Installers/BlocksBuilderInstaller")]
public class BlocksBuilderInstaller : ScriptableObjectInstaller<BlocksBuilderInstaller>
{
    [SerializeField] private Settings _settings;
   
    public override void InstallBindings()
    {
        Container.BindFactory<BlockCreationConfig, Block, BlockFactory>()
            .FromSubContainerResolve()
            .ByNewContextPrefab<BlockInstaller>(_settings.BlockPrefab)
            .AsSingle();

        Container.Bind<Settings>().FromInstance(_settings).AsSingle();

        Container.Bind<BlocksRegistry>().AsSingle();

        Container.BindInterfacesTo<BlocksBuilder>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public Block BlockPrefab;
        public int BlocksPerLevel;
        public float SpaceBetweenBlocks;
        public float SpaceBetweenLayers;
        public BlockAppearanceSettings BlockAppearanceSettings;
    }
}