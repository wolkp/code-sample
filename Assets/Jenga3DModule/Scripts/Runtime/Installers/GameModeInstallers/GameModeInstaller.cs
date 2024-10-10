using System;
using UnityEngine;
using Zenject;

public abstract class GameModeInstaller : ScriptableObjectInstaller
{
    [SerializeField] private GameMode _gameMode;

    public GameMode GameMode => _gameMode;

    public override void InstallBindings()
    {
        Container.QueueForInject(_gameMode);

        Container.Bind<GameMode>().FromInstance(_gameMode).AsSingle();
    }
}