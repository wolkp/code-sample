public class ObjectDeselectedSignal
{
    public ISelectable DeselectedObject { get; private set; }

    public ObjectDeselectedSignal(ISelectable deselectedObject)
    {
        DeselectedObject = deselectedObject;
    }
}