public class ObjectSelectedSignal
{
    public ISelectable SelectedObject { get; private set; }

    public ObjectSelectedSignal(ISelectable selectedObject)
    {
        SelectedObject = selectedObject;
    }
}