using UnityEngine;
using Zenject;

public abstract class GameObjectFactory<TConfig, T> : PlaceholderFactory<TConfig, T>
    where TConfig : GameObjectCreationConfig<T>
    where T : MonoBehaviour
{
    protected virtual void Initialize(TConfig config, T instance) { }

    public override T Create(TConfig config)
    {
        T instance = base.Create(config);
        instance.transform.SetParent(config.ParentTransform);
        instance.transform.localScale = Vector3.one;
        instance.transform.position = config.Position;
        instance.transform.rotation = config.Rotation;

        Initialize(config, instance);

        return instance;
    }
}

public abstract class GameObjectCreationConfig<T> where T : MonoBehaviour
{
    public Transform ParentTransform { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
}