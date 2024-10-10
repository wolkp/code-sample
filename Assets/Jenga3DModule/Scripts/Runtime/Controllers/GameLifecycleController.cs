#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameLifecycleController : IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;

    [Inject]
    public GameLifecycleController(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<RestartGameSignal>(OnRestartGameSignal);
        _signalBus.Subscribe<QuitGameSignal>(OnQuitGameSignal);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<RestartGameSignal>(OnRestartGameSignal);
        _signalBus.Unsubscribe<QuitGameSignal>(OnQuitGameSignal);
    }

    private void OnRestartGameSignal(RestartGameSignal signal)
    {
        RestartGame();
    }

    private void OnQuitGameSignal(QuitGameSignal signal)
    {
        QuitGame();
    }

    /// <summary>
    /// Basic restart by reloading the scene
    /// </summary>
    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR

        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}