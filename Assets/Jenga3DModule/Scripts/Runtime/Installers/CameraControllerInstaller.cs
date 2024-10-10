using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CameraControllerInstaller", menuName = "Installers/CameraControllerInstaller")]
public class CameraControllerInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Settings _settings;
   
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CameraController>().AsSingle();

        Container.Bind<Settings>().FromInstance(_settings).AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public Vector3 CameraOffset;
        public float RotationSpeed;
        public float ResetRotationDuration;
        public float MoveDuration;
        public float DistanceFromStack;
    }
}