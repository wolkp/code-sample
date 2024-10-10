using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GradeDataProviderInstaller", menuName = "Installers/GradeDataProviderInstaller")]
public class GradeDataProviderInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        Container.Bind<Settings>().FromInstance(_settings);
        Container.Bind<IDataProvider>().To<GradeDataProvider>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public string ApiUrl;
    }
}
