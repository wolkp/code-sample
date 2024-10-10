public class StackCreatedSignal
{
    public Stack CreatedStack { get; private set; }
    public GradeData[] GradeData { get; private set; }

    public StackCreatedSignal(Stack createdStack, GradeData[] gradeData)
    {
        CreatedStack = createdStack;
        GradeData = gradeData;
    }
}