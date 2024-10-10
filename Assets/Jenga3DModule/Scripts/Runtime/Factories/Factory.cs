using Zenject;

public abstract class Factory<T, TConfig> : PlaceholderFactory<T, TConfig>
{
    public abstract T Create(TConfig config);
}