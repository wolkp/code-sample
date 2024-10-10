using System.Collections.Generic;
using System.Linq;

public class BlocksRegistry : Registry<Block>
{
    public IEnumerable<Block> GetBlocksFromStack(Stack stack)
    {
        var allBlocks = GetAllItems();
        var blocksFromStack = allBlocks.Where(block => block.Data.Stack == stack);

        return blocksFromStack;
    }
}