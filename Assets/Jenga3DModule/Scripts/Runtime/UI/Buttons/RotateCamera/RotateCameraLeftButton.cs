public class RotateCameraLeftButton : RotateCameraButton
{
    protected override RotateCameraSignal CreateSignal()
    {
        return new RotateCameraSignal(ECameraRotationDirection.RotateLeft);
    }
}