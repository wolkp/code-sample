using System;
using UnityEngine;
using Zenject;

public class BlocksLabelBuilder : IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly BlockLabelFactory _blockLabelFactory;
    private readonly BlocksLabelBuilderInstaller.Settings _builderSettings;

    public BlocksLabelBuilder(
        SignalBus signalBus,
        BlockLabelFactory blockLabelFactory,
        BlocksLabelBuilderInstaller.Settings builderSettings)
    {
        _signalBus = signalBus;
        _blockLabelFactory = blockLabelFactory;
        _builderSettings = builderSettings;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<BlockCreatedSignal>(OnBlockCreatedSignal);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<BlockCreatedSignal>(OnBlockCreatedSignal);
    }

    private void OnBlockCreatedSignal(BlockCreatedSignal signal)
    {
        BuildLabelsForBlock(signal.CreatedBlock);
    }

    private void BuildLabelsForBlock(Block block)
    {
        var offset = _builderSettings.BlockLabelOffset;
        var blockSize = block.View.Size;

        CreateLabelForBlock(block, new Vector3(offset.x, offset.y, -offset.z), Quaternion.identity);
        CreateLabelForBlock(block, new Vector3(offset.x, offset.y, blockSize.z + offset.z), Quaternion.Euler(0, 180, 0));
    }

    private void CreateLabelForBlock(Block block, Vector3 offset, Quaternion rotation)
    {
        BlockLabelCreationConfig blockLabelCreationConfig = new()
        {
            Block = block,
            Position = block.transform.position + offset,
            Rotation = rotation,
            ParentTransform = block.transform
        };

        _blockLabelFactory.Create(blockLabelCreationConfig);
    }
}