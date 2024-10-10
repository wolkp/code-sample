public class RotateCameraSignal
{
    public ECameraRotationDirection RotationDirection { get; }

    public RotateCameraSignal(ECameraRotationDirection rotationDirection)
    {
        RotationDirection = rotationDirection;
    }
}