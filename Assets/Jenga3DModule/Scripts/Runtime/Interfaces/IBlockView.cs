using UnityEngine;

public interface IBlockView
{
    Vector3 Size { get; }
    void ApplyAppearanceFromConfig(BlockCreationConfig config);
    void OnSelect();
    void OnDeselect();
}