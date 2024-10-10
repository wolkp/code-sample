using TMPro;
using UnityEngine;
using Zenject;

public class BlockLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _labelText;

    [Inject]
    public void Construct(
        BlockLabelCreationConfig blockLabelCreationConfig, BlocksBuilderInstaller.Settings blockBuilderSettings)
    {
        var blockData = blockLabelCreationConfig.Block.Data;
        var gradeData = blockData.GradeData;
        var appearanceSettings = blockBuilderSettings.BlockAppearanceSettings;

        var masteryLevel = gradeData.mastery;
        var appearanceData = appearanceSettings.GetAppearanceData(masteryLevel);
        var masteryLevelText = appearanceData.MasteryLevelName;

        _labelText.text = masteryLevelText;
    }
}