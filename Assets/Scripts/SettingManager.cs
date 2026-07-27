using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Expandable] public SettingPreset currentSettingPreset;
    [Expandable] public SettingPreset defaultSettingPreset;

    [CurveRange(0,0,1,1,EColor.Violet)] public AnimationCurve volumeCurve;
    
    public AudioMixerGroup masterGroup;
    public Slider masterSlider;
    public AudioMixerGroup sfxGroup;
    public Slider sfxSlider;
    public AudioMixerGroup musicGroup;
    public Slider musicSlider;
        
    public void OnEnable()
    {
        masterSlider.onValueChanged.AddListener((arg) =>
        {
            SetVolume(masterSlider.value, masterGroup);
            currentSettingPreset.masterVolume = masterSlider.value;
        });
        sfxSlider.onValueChanged.AddListener((arg) =>
        {
            SetVolume(sfxSlider.value, sfxGroup);
            currentSettingPreset.sfxVolume = sfxSlider.value;
        });
        musicSlider.onValueChanged.AddListener((arg) =>
        {
            SetVolume(musicSlider.value, musicGroup);
            currentSettingPreset.musicVolume = musicSlider.value;
        });
    }
    public void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener((arg) =>
        {
            SetVolume(masterSlider.value, masterGroup);
            currentSettingPreset.masterVolume = masterSlider.value;
        });
        sfxSlider.onValueChanged.RemoveListener((arg) =>
        {
            SetVolume(sfxSlider.value, sfxGroup);
            currentSettingPreset.sfxVolume = sfxSlider.value;
        });
        musicSlider.onValueChanged.RemoveListener((arg) =>
        {
            SetVolume(musicSlider.value, musicGroup);
            currentSettingPreset.musicVolume = musicSlider.value;
        });
    }

    public void Start()
    {
        ApplyCurrentPreset();
    }
    public void QuitApp()
    {
        Application.Quit();
    }

    [Button]
    public void ApplyCurrentPreset()
    {
        SetVolume(currentSettingPreset.masterVolume,masterGroup);
        masterSlider.value = currentSettingPreset.masterVolume;
        SetVolume(currentSettingPreset.sfxVolume,sfxGroup);
        sfxSlider.value = currentSettingPreset.sfxVolume;
        SetVolume(currentSettingPreset.musicVolume,musicGroup);
        musicSlider.value = currentSettingPreset.musicVolume;
    }
    public void SetVolume(float volume, AudioMixerGroup group)
    {
        group.audioMixer.SetFloat(group.name , volumeCurve.Evaluate(volume) * 90 - 80);
    }
    [Button]
    public void ResetCurrentPreset()
    {
        currentSettingPreset.masterVolume = defaultSettingPreset.masterVolume;
        currentSettingPreset.sfxVolume = defaultSettingPreset.sfxVolume;
        currentSettingPreset.musicVolume = defaultSettingPreset.musicVolume;
    }
}