using Jenga3DModule.Scripts.Runtime.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class BlockDetailsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private TMP_Text _gradeDomainText;
    [SerializeField] private TMP_Text _clusterText;
    [SerializeField] private TMP_Text _standardText;

    private SignalBus _signalBus;
    private BlockDetailsPanelInstaller.Settings _settings;
    private CameraController _cameraController;
    private CanvasScaler _canvasScaler;
    private RectTransform _rectTransform;

    private Camera Camera => _cameraController.Camera;
    private Vector2 CanvasScaleMultiplier => UIScalingUtility.GetCanvasScale(_canvasScaler);
    private Vector2 ScaledPanelSize => _rectTransform.sizeDelta * CanvasScaleMultiplier;
    private Vector2 ScaledOffset => _settings.PanelOffset * CanvasScaleMultiplier;

    [Inject]
    public void Construct(
        SignalBus signalBus,
        BlockDetailsPanelInstaller.Settings settings,
        CameraController cameraController,
        CanvasScaler canvasScaler)
    {
        _signalBus = signalBus;
        _settings = settings;
        _cameraController = cameraController;
        _canvasScaler = canvasScaler;

        _signalBus.Subscribe<ObjectSelectedSignal>(OnObjectSelectedSignal);
        _signalBus.Subscribe<ObjectDeselectedSignal>(OnObjectDeselectedSignal);
    }

    private void Awake()
    {
        _rectTransform = _content.GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ObjectSelectedSignal>(OnObjectSelectedSignal);
        _signalBus.Unsubscribe<ObjectDeselectedSignal>(OnObjectDeselectedSignal);
    }

    private void OnObjectSelectedSignal(ObjectSelectedSignal signal)
    {
        if (signal.SelectedObject is Block selectedBlock)
        {
            HandleSelection(selectedBlock);
        }
    }

    private void OnObjectDeselectedSignal(ObjectDeselectedSignal signal)
    {
        if (signal.DeselectedObject is Block deselectedBlock)
        {
            HandleDeselection(deselectedBlock);
        }
    }

    private void HandleSelection(Block selectedBlock)
    {
        Toggle(true);
        SetPositionForBlock(selectedBlock);
        ApplyDataFromBlock(selectedBlock);
    }

    private void HandleDeselection(Block deselectedBlock)
    {
        Toggle(false);
    }

    private void Toggle(bool enable)
    {
        _content.SetActive(enable);
    }

    private void SetPositionForBlock(Block selectedBlock)
    {
        var blockLayerData = selectedBlock.Data.LayerData;
        var screenPosition = (Vector2)Camera.WorldToScreenPoint(blockLayerData.CenterPoint);
        var panelPosition = screenPosition + GetPanelOffset(screenPosition);

        // Clamp the position to ensure it stays within the screen boundaries
        var clampedPosition = ClampToScreen(panelPosition, ScaledPanelSize, CanvasScaleMultiplier);

        _rectTransform.position = clampedPosition;
    }

    private Vector2 GetPanelOffset(Vector2 screenPosition)
    {
        var screenCenter = Screen.width / 2;
        
        // If block's stack is on the right side of the screen, position the panel on the left side (and vice-versa)
        if (screenPosition.x > screenCenter)
        {
            var offsetFlippedHorizontally = new Vector2(-ScaledOffset.x, ScaledOffset.y);
            return offsetFlippedHorizontally;
        }
        else
        {
            var originalOffset = ScaledOffset;
            return originalOffset;
        }
    }

    private Vector2 ClampToScreen(Vector2 position, Vector2 scaledPanelSize, Vector2 canvasScaleMultiplier)
    {
        var scaledScreenMargins = _settings.ScreenMargins * canvasScaleMultiplier;

        var scaledScreenWidth = Screen.width;
        var scaledScreenHeight = Screen.height;

        var minX = scaledPanelSize.x / 2 + scaledScreenMargins.x;
        var maxX = scaledScreenWidth - minX;
        var minY = scaledPanelSize.y / 2 + scaledScreenMargins.y;
        var maxY = scaledScreenHeight;

        var clampedX = Mathf.Clamp(position.x, minX, maxX);
        var clampedY = Mathf.Clamp(position.y, minY, maxY);

        return new Vector2(clampedX, clampedY);
    }

    private void ApplyDataFromBlock(Block selectedBlock)
    {
        var blockGradeData = selectedBlock.Data.GradeData;

        _gradeDomainText.text = $"{blockGradeData.grade}: {blockGradeData.domain}";
        _clusterText.text = $"{blockGradeData.cluster}";
        _standardText.text = $"{blockGradeData.standardid}:\n{blockGradeData.standarddescription}";
    }
}