using UnityEngine;

[CreateAssetMenu(fileName = "SettingPreset", menuName = "SettingPreset")]
public class SettingPreset : ScriptableObject
{
    public float masterVolume;
    public float sfxVolume;
    public float musicVolume;
}