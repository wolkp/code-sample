using Zenject;

public abstract class RotateCameraButton : HoldableButton
{
    private SignalBus _signalBus;

    protected abstract RotateCameraSignal CreateSignal();

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    protected override void OnButtonClicked()
    {
        var signal = CreateSignal();
        _signalBus.Fire(signal);
    }
}