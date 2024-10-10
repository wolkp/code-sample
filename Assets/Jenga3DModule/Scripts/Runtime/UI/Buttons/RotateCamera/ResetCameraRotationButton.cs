public class ResetCameraRotationButton : RotateCameraButton
{
    protected override RotateCameraSignal CreateSignal()
    {
        return new RotateCameraSignal(ECameraRotationDirection.ResetRotation);
    }
}