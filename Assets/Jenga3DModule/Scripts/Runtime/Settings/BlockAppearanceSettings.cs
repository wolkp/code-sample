using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BlockAppearanceSettings", menuName = "Settings/BlockAppearanceSettings")]
public class BlockAppearanceSettings : ScriptableObject
{
    [Serializable]
    public class AppearanceData
    {
        public int MasteryLevel;
        public string MasteryLevelName;
        public string MaterialName;
        public Material Material;
    }

    [SerializeField] private AppearanceData[] _appearanceData;

    public AppearanceData GetAppearanceData(int masteryLevel)
    {
        return Array.Find(_appearanceData, data => data.MasteryLevel == masteryLevel);
    }
}