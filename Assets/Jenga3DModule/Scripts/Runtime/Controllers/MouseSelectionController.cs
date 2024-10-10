using Lagrange;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class MouseSelectionController : MonoBehaviour
{
    private SignalBus _signalBus;
    private CameraController _cameraController;
    private GraphicRaycaster _graphicRaycaster;
    private PointerEventData _pointerEventData;
    private ISelectable _currentlySelected;

    [Inject]
    public void Construct(
        SignalBus signalBus,
        CameraController cameraController,
        GraphicRaycaster graphicRaycaster)
    {
        _signalBus = signalBus;
        _cameraController = cameraController;
        _graphicRaycaster = graphicRaycaster;
    }

    private void Awake()
    {
        _pointerEventData = new PointerEventData(EventSystem.current);
    }

    private void Update()
    {
        var hasUpdated = UpdateRaycastSelection();

        if (hasUpdated == false && _currentlySelected != null)
        {
            ObjectDeselected(_currentlySelected);
        }
    }

    private bool UpdateRaycastSelection()
    {
        if (IsMouseOverUI())
            return false;

        var ray = _cameraController.Camera.ScreenPointToRay(Input.mousePosition);
        var layerMask = LayerUtility.LayerToLayerMask(LayerUtility.Default);
        var raycastHitSelectable = false;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            var selectable = hit.transform.GetComponent<ISelectable>();

            if (selectable != null)
            {
                HandleSelection(selectable);
                raycastHitSelectable = true;
            }
        }

        return raycastHitSelectable;
    }

    private void HandleSelection(ISelectable newSelection)
    {
        if (_currentlySelected != null && _currentlySelected != newSelection)
        {
            ObjectDeselected(_currentlySelected);
        }

        if (_currentlySelected != newSelection)
        {
            ObjectSelected(newSelection);
        }
    }

    private void ObjectSelected(ISelectable selection)
    {
        _signalBus.Fire(new ObjectSelectedSignal(selection));
        _currentlySelected = selection;
    }

    private void ObjectDeselected(ISelectable selection)
    {
        _signalBus.Fire(new ObjectDeselectedSignal(selection));
        _currentlySelected = null;
    }

    private bool IsMouseOverUI()
    {
        var results = new List<RaycastResult>();
        _pointerEventData.position = Input.mousePosition;
        _graphicRaycaster.Raycast(_pointerEventData, results);

        return results.Count > 0;
    }
}