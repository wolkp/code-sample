using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class HoldableButton : Button, IPointerDownHandler, IPointerUpHandler
{
    private CancellationTokenSource _holdCancellationTokenSource;
    private bool _isPressed;

    protected abstract void OnButtonClicked();

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        _isPressed = true;
        _holdCancellationTokenSource = new CancellationTokenSource();

        HandleHoldAsync(_holdCancellationTokenSource.Token).Forget();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        _isPressed = false;
        _holdCancellationTokenSource?.Cancel();
    }

    private async UniTask HandleHoldAsync(CancellationToken cancellationToken)
    {
        while(_isPressed)
        {
            OnHold();
            await UniTask.Yield(cancellationToken);
        }
    }

    private void OnHold()
    {
        OnButtonClicked();
    }
}