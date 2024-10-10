using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameModesInstaller", menuName = "Installers/GameModesInstaller")]
public class GameModesInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        Container.Bind<Settings>().FromInstance(_settings).AsSingle();

        Container.Bind<GameModesRegistry>().AsSingle();

        var allGameModes = new List<GameMode>();
        foreach (var gameModeInstaller in _settings.GameModeInstallers)
        {
            Container.Inject(gameModeInstaller.GameMode);
            Container.Bind<GameMode>().FromInstance(gameModeInstaller.GameMode).AsSingle();

            allGameModes.Add(gameModeInstaller.GameMode);
        }

        Container.Bind<IEnumerable<GameMode>>().FromInstance(allGameModes).AsSingle();

        Container.BindInterfacesAndSelfTo<GameModesInitializer>().AsSingle().NonLazy();
    }

    [Serializable]
    public class Settings
    {
        public List<GameModeInstaller> GameModeInstallers;
    }
}