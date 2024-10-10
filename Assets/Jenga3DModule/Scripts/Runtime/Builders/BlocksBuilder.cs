using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class BlocksBuilder : IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly BlockFactory _blockFactory;
    private readonly BlocksBuilderInstaller.Settings _builderSettings;

    public BlocksBuilder(
        SignalBus signalBus,
        BlockFactory blockFactory,
        BlocksBuilderInstaller.Settings builderSettings)
    {
        _signalBus = signalBus;
        _blockFactory = blockFactory;
        _builderSettings = builderSettings;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<StackCreatedSignal>(OnStackCreatedSignal);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<StackCreatedSignal>(OnStackCreatedSignal);
    }

    private void OnStackCreatedSignal(StackCreatedSignal signal)
    {
        GenerateBlocksForStack(signal.CreatedStack, signal.GradeData);
    }

    private void GenerateBlocksForStack(Stack stack, GradeData[] data)
    {
        var blocksPerLayerCount = _builderSettings.BlocksPerLevel;
        var totalLayersCount = Mathf.CeilToInt((float)data.Length / blocksPerLayerCount);

        for (var layerIndex = 0; layerIndex < totalLayersCount; layerIndex++)
        {
            float layerY = layerIndex * _builderSettings.SpaceBetweenLayers;
            var gradeDataForLayer = data.Skip(layerIndex * blocksPerLayerCount).Take(blocksPerLayerCount).ToArray();
            GenerateLayer(stack, gradeDataForLayer, layerY, layerIndex);
        }
    }

    private void GenerateLayer(Stack stack, GradeData[] gradeDataForLayer, float layerY, int layerIndex)
    {
        GameObject layerParent = CreateLayerParent(stack, layerY, layerIndex);
        var blockSize = GetBlockSize();
        var blocksOffset = _builderSettings.SpaceBetweenBlocks + blockSize.z;
        var layerCenterPoint = CalculateLayerCenterPoint(layerParent, blocksOffset);
        var layerData = new BlockLayerData(layerCenterPoint);

        // Positioning variables
        var xOffset = 0f;
        var zOffset = 0f;

        foreach (var gradeData in gradeDataForLayer)
        {
            var blockParent = layerParent.transform;
            var blockPosition = blockParent.transform.position + new Vector3(xOffset, 0, zOffset);

            Block block = CreateBlock(stack, gradeData, layerData, blockPosition, blockParent);

            _signalBus.Fire(new BlockCreatedSignal(block));

            // Move to the next position
            zOffset += blocksOffset;
            if (zOffset >= _builderSettings.BlocksPerLevel * blocksOffset)
            {
                zOffset = 0;
                xOffset += blockSize.z;
            }
        }

        RotateLayerIfNeeded(layerIndex, layerParent, layerCenterPoint);
    }

    private void RotateLayerIfNeeded(int layerIndex, GameObject layerParent, Vector3 layerCenterPoint)
    {
        if (layerIndex % 2 != 0)
        {
            layerParent.transform.RotateAround(layerCenterPoint, Vector3.up, 90);
        }
    }

    private Vector3 CalculateLayerCenterPoint(GameObject layerParent, float blocksOffset)
    {
        var layerWidth = _builderSettings.BlocksPerLevel * blocksOffset - _builderSettings.SpaceBetweenBlocks;
        var centerPoint = layerParent.transform.position + new Vector3(layerWidth / 2, 0, layerWidth / 2);

        return centerPoint;
    }

    private Vector3 GetBlockSize()
    {
        var blockPrefab = _builderSettings.BlockPrefab;
        var blockView = blockPrefab.GetComponent<IBlockView>();
        var blockSize = blockView.Size;

        return blockSize;
    }

    private GameObject CreateLayerParent(Stack stack, float layerY, int layerIndex)
    {
        GameObject layerParent = new GameObject($"{StringConstants.LayerObjectNamePrefix} {layerIndex}");
        layerParent.transform.parent = stack.transform;
        layerParent.transform.localPosition = new Vector3(0, layerY, 0);

        return layerParent;
    }

    private Block CreateBlock(Stack stack, GradeData gradeData, BlockLayerData layerData,
        Vector3 position, Transform parentTransform)
    {
        BlockCreationConfig blockCreationConfig = new()
        {
            Stack = stack,
            GradeData = gradeData,
            LayerData = layerData,
            AppearanceSettings = _builderSettings.BlockAppearanceSettings,
            Position = position,
            ParentTransform = parentTransform
        };

        return _blockFactory.Create(blockCreationConfig);
    }
}