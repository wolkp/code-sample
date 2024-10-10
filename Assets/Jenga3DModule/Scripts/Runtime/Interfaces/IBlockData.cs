public interface IBlockData
{
    Stack Stack { get; }
    GradeData GradeData { get; }
    BlockLayerData LayerData { get; }
    void SetDataFromCreationConfig(BlockCreationConfig config);
}