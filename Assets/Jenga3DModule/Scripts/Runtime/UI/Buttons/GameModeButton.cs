using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameModeButton : Button
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _label;

    [Inject]
    public void Construct(GameModeButtonCreationConfig gameModeButtonCreationConfig)
    {
        var iconSprite = gameModeButtonCreationConfig.ModeIcon;
        var labelText = gameModeButtonCreationConfig.ModeName;

        _iconImage.overrideSprite = iconSprite;
        _label.text = labelText;
    }
}