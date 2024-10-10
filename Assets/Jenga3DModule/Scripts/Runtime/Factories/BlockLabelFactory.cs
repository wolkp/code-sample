public class BlockLabelFactory : GameObjectFactory<BlockLabelCreationConfig, BlockLabel>
{
}

public class BlockLabelCreationConfig : GameObjectCreationConfig<BlockLabel>
{
    public Block Block { get; set; }
}