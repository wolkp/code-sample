using UnityEngine;
public abstract class GameMode : ScriptableObject
{
    [SerializeField] private string _modeName;
    [SerializeField] Sprite _modeIcon;

    public string ModeName => _modeName;
    public Sprite ModeIcon => _modeIcon;

    public abstract void Activate();
}