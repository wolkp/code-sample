public class BlockFactory : GameObjectFactory<BlockCreationConfig, Block>
{
    protected override void Initialize(BlockCreationConfig config, Block instance)
    {
        base.Initialize(config, instance);
        instance.InitializeFromConfig(config);
    }
}

public class BlockCreationConfig : GameObjectCreationConfig<Block>
{
    public Stack Stack { get; set; }
    public GradeData GradeData { get; set; }
    public BlockLayerData LayerData { get; set; }
    public BlockAppearanceSettings AppearanceSettings { get; set; }
}