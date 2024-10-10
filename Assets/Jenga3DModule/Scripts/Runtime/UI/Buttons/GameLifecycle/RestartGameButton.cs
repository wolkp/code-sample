using UnityEngine.UI;
using Zenject;

public class RestartGameButton : Button
{
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    protected override void Awake()
    {
        base.Awake();
        onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _signalBus.Fire(new RestartGameSignal());
    }
}
