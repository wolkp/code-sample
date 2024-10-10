using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

public class CameraController : TransitionedTransform, IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly StackFocusController _stackFocusController;
    private readonly CameraControllerInstaller.Settings _settings;

    private Quaternion _initialCameraRotation;

    public Camera Camera { get; private set; }

    protected override Transform Transform => Camera.transform;

    public CameraController(
        SignalBus signalBus,
        StackFocusController stackFocusController,
        CameraControllerInstaller.Settings settings)
    {
        _signalBus = signalBus;
        _stackFocusController = stackFocusController;
        _settings = settings;
    }

    public void Initialize()
    {
        InitializeCamera();

        _signalBus.Subscribe<RotateCameraSignal>(OnCameraRotationSignal);
        _signalBus.Subscribe<FocusStackSignal>(OnFocusStackSignal);
    }

    public void Dispose()
    {
        CancelOngoingTransitions();
        DisposeOngoingTransitions();

        _signalBus.Unsubscribe<RotateCameraSignal>(OnCameraRotationSignal);
        _signalBus.Unsubscribe<FocusStackSignal>(OnFocusStackSignal);
    }

    private void InitializeCamera()
    {
        Camera = Camera.main;
        _initialCameraRotation = Transform.rotation;
    }

    private void OnCameraRotationSignal(RotateCameraSignal signal)
    {
        RotateCamera(signal.RotationDirection);
    }

    private void OnFocusStackSignal(FocusStackSignal signal)
    {
        ResetCameraView();
    }

    private void ResetCameraView()
    {
        ResetCameraRotation();
        MoveCameraToFrontOfStack();
    }

    private void RotateCamera(ECameraRotationDirection rotationDirection)
    {
        switch(rotationDirection)
        {
            case ECameraRotationDirection.RotateLeft:
                RotateCameraLeft();
                break;
            case ECameraRotationDirection.RotateRight:
                RotateCameraRight();
                break;
            case ECameraRotationDirection.ResetRotation:
                ResetCameraView();
                break;
        }
    }

    private void RotateCameraLeft()
    {
        RotateCameraByAngle(_settings.RotationSpeed * Time.deltaTime);
    }

    private void RotateCameraRight()
    {
        RotateCameraByAngle(-_settings.RotationSpeed * Time.deltaTime);
    }

    private void RotateCameraByAngle(float rotationAngle)
    {
        var focusedStackTransform = _stackFocusController.CurrentFocusedStack.transform;
        Transform.RotateAround(focusedStackTransform.position, Vector3.up, rotationAngle);
    }

    private void ResetCameraRotation()
    {
        var currentRotation = Transform.rotation;
        var desiredRotation = _initialCameraRotation;

        CancelOngoingRotationTransition();
        _rotationCancellationTokenSource = new CancellationTokenSource();
        RotateSmoothly(currentRotation, desiredRotation, 
            _settings.ResetRotationDuration, _rotationCancellationTokenSource.Token)
            .Forget();
    }

    private void MoveCameraToFrontOfStack()
    {
        var focusedStackTransform = _stackFocusController.CurrentFocusedStack.transform;

        var desiredPosition = focusedStackTransform.position - focusedStackTransform.forward * _settings.DistanceFromStack;
        desiredPosition += _settings.CameraOffset;

        CancelOngoingMovementTransition();
        _movementCancellationTokenSource = new CancellationTokenSource();
        MoveSmoothly(desiredPosition, 
            _settings.MoveDuration, _movementCancellationTokenSource.Token)
            .Forget();
    }
}