using Zenject;

/// <summary>
/// Acts as a facade, provides centralized access to block's data, physics, view
/// </summary>
public class Block : RegisteredGameObject<Block, BlocksRegistry>, ISelectable
{
    public IBlockData Data { get; private set; }
    public IBlockPhysics Physics { get; private set; }
    public IBlockView View { get; private set; }

    [Inject]
    public void Construct(
        BlockCreationConfig blockCreationConfig,
        IBlockData data,
        IBlockPhysics physics,
        IBlockView view)
    {
        Data = data;
        Physics = physics;
        View = view;
    }

    public void InitializeFromConfig(BlockCreationConfig blockCreationConfig)
    {
        Data.SetDataFromCreationConfig(blockCreationConfig);
        View.ApplyAppearanceFromConfig(blockCreationConfig);
    }
}