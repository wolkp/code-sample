public class GradeButtonFactory : ButtonFactory<GradeButtonCreationConfig, SwitchGradeButton>
{
}

public class GradeButtonCreationConfig : ButtonCreationConfig<SwitchGradeButton>
{
    public Stack Stack { get; set; }
}