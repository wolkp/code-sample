using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SwitchGradeButtonsInstaller", menuName = "Installers/SwitchGradeButtonsInstaller")]
public class SwitchGradeButtonsInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Settings _settings;
   
    public override void InstallBindings()
    {
        Container.BindFactory<GradeButtonCreationConfig, SwitchGradeButton, GradeButtonFactory>()
            .FromComponentInNewPrefab(_settings.ButtonPrefab)
            .AsSingle();

        Container.Bind<Settings>().FromInstance(_settings).AsSingle();

        Container.BindInterfacesTo<SwitchGradeButtonsBuilder>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public SwitchGradeButton ButtonPrefab;
    }
}