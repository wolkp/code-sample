using System;
using UnityEngine.UI;

public abstract class ButtonFactory<TConfig, T> : GameObjectFactory<TConfig, T>
    where TConfig : ButtonCreationConfig<T>
    where T : Button
{
    protected override void Initialize(TConfig config, T instance)
    {
        instance.onClick.AddListener(() => config.ActionOnClick?.Invoke());
    }
}

public abstract class ButtonCreationConfig<T> : GameObjectCreationConfig<T> where T : Button
{
    public Action ActionOnClick { get; set; }
}