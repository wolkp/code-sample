using UnityEngine;
using Zenject;

public class SwitchGradeButtonsBuilder : ButtonsBuilder
{
    private readonly SignalBus _signalBus;
    private readonly GradeButtonFactory _gradeButtonsFactory;
    private readonly StackFocusController _stackFocusController;

    public SwitchGradeButtonsBuilder(
        SignalBus signalBus,
        GradeButtonFactory gradeButtonsFactory,
        SwitchGradeButtonsParent buttonsParent,
        StackFocusController stackFocusController)
        : base(buttonsParent)
    {
        _signalBus = signalBus;
        _gradeButtonsFactory = gradeButtonsFactory;
        _stackFocusController = stackFocusController;
    }

    protected override void SetupButtons(Transform buttonsParentTransform)
    {
        _signalBus.Subscribe<StackCreatedSignal>(x => CreateSwitchGradeButton(x.CreatedStack, buttonsParentTransform));
    }

    private SwitchGradeButton CreateSwitchGradeButton(Stack stack, Transform buttonsParentTransform)
    {
        GradeButtonCreationConfig gameModeButtonCreationConfig = new()
        {
            Stack = stack,
            ActionOnClick = () => _stackFocusController.FocusOnStack(stack),
            ParentTransform = buttonsParentTransform
        };

        SwitchGradeButton createdButton = _gradeButtonsFactory.Create(gameModeButtonCreationConfig);

        return createdButton;
    }
}