public class RotateCameraRightButton : RotateCameraButton
{
    protected override RotateCameraSignal CreateSignal()
    {
        return new RotateCameraSignal(ECameraRotationDirection.RotateRight);
    }
}