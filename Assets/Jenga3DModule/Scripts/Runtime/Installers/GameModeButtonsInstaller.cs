using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameModeButtonsInstaller", menuName = "Installers/GameModeButtonsInstaller")]
public class GameModeButtonsInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Settings _settings;
   
    public override void InstallBindings()
    {
        Container.BindFactory<GameModeButtonCreationConfig, GameModeButton, GameModeButtonFactory>()
            .FromComponentInNewPrefab(_settings.ButtonPrefab)
            .AsSingle();

        Container.BindInterfacesTo<GameModeButtonsBuilder>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public GameModeButton ButtonPrefab;
    }
}