using UnityEngine;
using Zenject;

public class BlockView : MonoBehaviour, IBlockView
{
    [SerializeField] private Renderer _model;
    [SerializeField] private Outline _outline;

    public Vector3 Size => _model.bounds.size;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<ObjectSelectedSignal>(OnObjectSelectedSignal);
        _signalBus.Subscribe<ObjectDeselectedSignal>(OnObjectDeselectedSignal);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ObjectSelectedSignal>(OnObjectSelectedSignal);
        _signalBus.Unsubscribe<ObjectDeselectedSignal>(OnObjectDeselectedSignal);
    }

    public void ApplyAppearanceFromConfig(BlockCreationConfig blockCreationConfig)
    {
        var gradeData = blockCreationConfig.GradeData;
        var masteryLevel = gradeData.mastery;
        var appearanceData = blockCreationConfig.AppearanceSettings.GetAppearanceData(masteryLevel);

        ApplyMaterial(appearanceData.Material);
    }

    public void OnSelect()
    {
        ToggleOutline(true);
    }

    public void OnDeselect()
    {
        ToggleOutline(false);
    }

    public void ToggleOutline(bool enable)
    {
        _outline.enabled = enable;
    }

    private void OnObjectSelectedSignal(ObjectSelectedSignal signal)
    {
        if (signal.SelectedObject is Block selectedBlock && 
            selectedBlock.View is BlockView blockView && blockView == this)
        {
            OnSelect();
        }
    }

    private void OnObjectDeselectedSignal(ObjectDeselectedSignal signal)
    {
        if (signal.DeselectedObject is Block deselectedBlock &&
            deselectedBlock.View is BlockView blockView && blockView == this)
        {
            OnDeselect();
        }
    }

    private void ApplyMaterial(Material material)
    {
        _model.sharedMaterial = material;
    }
}