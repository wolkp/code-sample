using UnityEngine;
using Zenject;

public abstract class RegisteredGameObject<T, TRegistry> : MonoBehaviour 
    where T: RegisteredGameObject<T, TRegistry>
    where TRegistry: Registry<T>
{
    private Registry<T> _registry;

    [Inject]
    public void Construct(TRegistry registry)
    {
        _registry = registry;
    }

    protected virtual void Awake()
    {
        AddToRegistry();
    }

    protected virtual void OnDestroy()
    {
        RemoveFromRegistry();
    }

    private void AddToRegistry()
    {
        _registry.AddItem((T)this);
    }

    private void RemoveFromRegistry()
    {
        _registry.RemoveItem((T)this);
    }
}