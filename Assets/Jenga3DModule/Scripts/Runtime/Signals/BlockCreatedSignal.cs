public class BlockCreatedSignal
{
    public Block CreatedBlock { get; private set; }

    public BlockCreatedSignal(Block createdBlock)
    {
        CreatedBlock = createdBlock;
    }
}