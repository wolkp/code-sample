public class StackLabelFactory : GameObjectFactory<StackLabelCreationConfig, StackLabel>
{
}

public class StackLabelCreationConfig : GameObjectCreationConfig<StackLabel>
{
    public Stack Stack { get; set; }
}