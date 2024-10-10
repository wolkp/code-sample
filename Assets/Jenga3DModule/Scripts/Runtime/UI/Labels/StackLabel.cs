using TMPro;
using UnityEngine;
using Zenject;

public class StackLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _labelText;
    [SerializeField] private GameObject _stackInFocusIndicator;

    private Stack _stack;

    [Inject]
    public void Construct(
        SignalBus signalBus, 
        StackLabelCreationConfig stackLabelCreationConfig)
    {
        _stack = stackLabelCreationConfig.Stack;
        signalBus.Subscribe<FocusStackSignal>(OnFocusStackSignal);
        SetLabelText();
    }

    private void OnFocusStackSignal(FocusStackSignal signal)
    {
        var shouldShowFocusIndicator = _stack == signal.Stack;
        _stackInFocusIndicator.SetActive(shouldShowFocusIndicator);
    }

    private void SetLabelText()
    {
        var gradeLevelText = _stack.GradeLevel;
        gradeLevelText = GradeLevelFormatter.GetParsedGradeLevelFull(gradeLevelText);

        _labelText.text = gradeLevelText;
    }
}