using UnityEngine;

public class GameModeButtonFactory : ButtonFactory<GameModeButtonCreationConfig, GameModeButton>
{
}

public class GameModeButtonCreationConfig : ButtonCreationConfig<GameModeButton>
{
    public Sprite ModeIcon { get; set; }
    public string ModeName { get; set; }
}