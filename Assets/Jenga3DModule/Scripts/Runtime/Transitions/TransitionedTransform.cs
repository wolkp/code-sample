using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public abstract class TransitionedTransform
{
    protected CancellationTokenSource _movementCancellationTokenSource;
    protected CancellationTokenSource _rotationCancellationTokenSource;

    protected abstract Transform Transform { get; }

    protected void CancelOngoingTransitions()
    {
        CancelOngoingMovementTransition();
        CancelOngoingRotationTransition();
    }

    protected void CancelOngoingMovementTransition()
    {
        _movementCancellationTokenSource?.Cancel();
    }

    protected void CancelOngoingRotationTransition()
    {
        _rotationCancellationTokenSource?.Cancel();
    }

    protected void DisposeOngoingTransitions()
    {
        _movementCancellationTokenSource?.Dispose();
        _rotationCancellationTokenSource?.Dispose();
    }

    protected async UniTask MoveSmoothly(Vector3 desiredPosition,
        float duration, CancellationToken cancellationToken)
    {
        await SmoothTransition(
            async (elapsedTime, t, cancellationToken) =>
            {
                Transform.position = Vector3.Lerp(Transform.position, desiredPosition, t); 

                await UniTask.Yield(cancellationToken);
            },
            duration,
            cancellationToken
        );
    }

    protected async UniTask RotateSmoothly(Quaternion currentRotation, Quaternion desiredRotation, 
        float duration, CancellationToken cancellationToken)
    {
        await SmoothTransition(
            async (elapsedTime, t, cancellationToken) =>
            {
                Transform.rotation = Quaternion.Slerp(currentRotation, desiredRotation, t);

                await UniTask.Yield(cancellationToken);
            },
            duration,
            cancellationToken
        );
    }

    private async UniTask SmoothTransition(Func<float, float, CancellationToken, UniTask> updateAction, 
        float duration, CancellationToken cancellationToken)
    {
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            var t = Mathf.Clamp01(elapsedTime / duration);
            await updateAction(elapsedTime, t, cancellationToken);
            elapsedTime += Time.deltaTime;

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
        }

        await updateAction(duration, 1f, cancellationToken);
    }
}