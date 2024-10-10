using UnityEngine;

public class BlockData : MonoBehaviour, IBlockData
{
    public Stack Stack { get; private set; }
    public GradeData GradeData { get; private set; }
    public BlockLayerData LayerData { get; private set; }

    public void SetDataFromCreationConfig(BlockCreationConfig config)
    {
        Stack = config.Stack;
        GradeData = config.GradeData;
        LayerData = config.LayerData;
    }
}