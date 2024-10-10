using UnityEngine;

public class GameModeButtonsBuilder : ButtonsBuilder
{
    private readonly GameModeButtonFactory _gameModeButtonsFactory;
    private readonly GameModesRegistry _gameModesRegistry;

    public GameModeButtonsBuilder(
        GameModeButtonsParent buttonsParent,
        GameModeButtonFactory gameModeButtonsFactory,
        GameModesRegistry gameModesRegistry)
        : base(buttonsParent)
    {
        _gameModeButtonsFactory = gameModeButtonsFactory;
        _gameModesRegistry = gameModesRegistry;
    }

    protected override void SetupButtons(Transform buttonsParentTransform)
    {
        var gameModes = _gameModesRegistry.GetAllItems();

        foreach (var gameMode in gameModes)
        {
            CreateGameModeButton(gameMode, buttonsParentTransform);
        }
    }

    private GameModeButton CreateGameModeButton(GameMode gameMode, Transform buttonsParentTransform)
    {
        GameModeButtonCreationConfig gameModeButtonCreationConfig = new()
        {
            ModeName = gameMode.ModeName,
            ModeIcon = gameMode.ModeIcon,
            ActionOnClick = gameMode.Activate,
            ParentTransform = buttonsParentTransform
        };

        GameModeButton createdButton = _gameModeButtonsFactory.Create(gameModeButtonCreationConfig);
        createdButton.transform.SetAsFirstSibling();

        return createdButton;
    }
}