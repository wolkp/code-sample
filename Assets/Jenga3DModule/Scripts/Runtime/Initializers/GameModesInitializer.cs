using System.Collections.Generic;
using Zenject;

public class GameModesInitializer : IInitializable
{
    private readonly GameModesRegistry _gameModesRegistry;
    private readonly IEnumerable<GameMode> _gameModes;

    public GameModesInitializer(
        GameModesRegistry gameModesRegistry,
        IEnumerable<GameMode> gameModes)
    {
        _gameModesRegistry = gameModesRegistry;
        _gameModes = gameModes;
    }

    public void Initialize()
    {
        foreach (var gameMode in _gameModes)
        {
            _gameModesRegistry.AddItem(gameMode);
        }
    }
}