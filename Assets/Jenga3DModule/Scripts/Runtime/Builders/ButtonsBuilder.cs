using UnityEngine;
using Zenject;

public abstract class ButtonsBuilder : IInitializable
{
    private readonly ButtonsParent _buttonsParent;

    protected abstract void SetupButtons(Transform buttonsParentTransform);

    public ButtonsBuilder(ButtonsParent buttonsParent)
    {
        _buttonsParent = buttonsParent;
    }

    public void Initialize()
    {
        SetupButtons(_buttonsParent.transform);
    }
}