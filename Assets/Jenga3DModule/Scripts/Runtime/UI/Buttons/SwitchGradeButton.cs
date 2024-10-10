using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SwitchGradeButton : Button
{
    [SerializeField] private TMP_Text _gradeText;
    [SerializeField] private GameObject _stackInFocusIndicator;

    private Stack _stack;

    [Inject]
    public void Construct(
        SignalBus signalBus,
        GradeButtonCreationConfig switchGradeButtonCreationConfig)
    {
        _stack = switchGradeButtonCreationConfig.Stack;
        signalBus.Subscribe<FocusStackSignal>(OnFocusStackSignal);
        SetGradeText();
    }

    private void OnFocusStackSignal(FocusStackSignal signal)
    {
        var shouldShowFocusIndicator = _stack == signal.Stack;
        _stackInFocusIndicator.SetActive(shouldShowFocusIndicator);
    }

    private void SetGradeText()
    {
        var gradeLevelText = _stack.GradeLevel;
        gradeLevelText = GradeLevelFormatter.GetParsedGradeLevelShortened(gradeLevelText);

        _gradeText.text = gradeLevelText;
    }
}