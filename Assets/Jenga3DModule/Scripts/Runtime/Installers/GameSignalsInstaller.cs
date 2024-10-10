using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "GameSignalsInstaller", menuName = "Installers/GameSignalsInstaller")]
public class GameSignalsInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        // Game lifecycle
        Container.DeclareSignal<RestartGameSignal>();
        Container.DeclareSignal<QuitGameSignal>();

        // Camera
        Container.DeclareSignal<RotateCameraSignal>();

        // Selection
        Container.DeclareSignal<ObjectSelectedSignal>();
        Container.DeclareSignal<ObjectDeselectedSignal>();

        // Stacks
        Container.DeclareSignal<StackCreatedSignal>();
        Container.DeclareSignal<AllStacksCreatedSignal>();
        Container.DeclareSignal<FocusStackSignal>();

        // Blocks
        Container.DeclareSignal<BlockCreatedSignal>();
    }
}