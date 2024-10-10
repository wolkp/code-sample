public class StackFactory : GameObjectFactory<StackCreationConfig, Stack>
{
}

public class StackCreationConfig : GameObjectCreationConfig<Stack>
{
    public string GradeLevel { get; set; }
}