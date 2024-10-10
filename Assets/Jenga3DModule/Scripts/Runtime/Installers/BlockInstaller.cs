using Zenject;

public class BlockInstaller : MonoInstaller
{
    private BlockCreationConfig _blockCreationConfig;

    [Inject]
    public void Init(BlockCreationConfig config)
    {
        _blockCreationConfig = config;
    }

    public override void InstallBindings()
    {
        Container.Bind<BlockCreationConfig>().FromInstance(_blockCreationConfig).AsSingle();

        Container.Bind<Block>().FromComponentOnRoot().AsSingle();
        Container.Bind<IBlockData>().To<BlockData>().FromComponentOnRoot().AsSingle();
        Container.Bind<IBlockPhysics>().To<BlockPhysics>().FromComponentOnRoot().AsSingle();
        Container.Bind<IBlockView>().To<BlockView>().FromComponentOnRoot().AsSingle();
    }
}
