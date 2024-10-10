using Zenject;

public class Stack : RegisteredGameObject<Stack, StacksRegistry>
{
    public string GradeLevel { get; private set; }

    [Inject]
    public void Construct(StackCreationConfig stackCreationConfig)
    {
        GradeLevel = stackCreationConfig.GradeLevel;
    }
}