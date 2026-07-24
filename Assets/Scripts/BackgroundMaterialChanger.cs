using System;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class BackgroundMaterialChanger : MonoBehaviour
{
    [SerializeField] private Material _material;
    public List<BackgroundColors>  backgroundColors = new List<BackgroundColors>();
    [SerializeField] private bool _applyOnStart;
    [SerializeField,ShowIf("_applyOnStart")] private int _applyOnStartIndex;

    public void Start()
    {
        if (_applyOnStart) ApplyColorsOnMaterial(_applyOnStartIndex);
    }

    public void ApplyColorsOnMaterial(int index)
    {
        if (backgroundColors.Count <= index) return;
        
        BackgroundColors color = backgroundColors[index];
        
        ApplyColor(color.low, color.middle, color.high, color.fadeTime);
    }

    public void ApplyColor(Color low, Color middle, Color high, float fadeTime)
    {
        DOTween.To(() => _material.GetColor("_LowColor"), x => _material.SetColor("_LowColor", x), low, fadeTime);
        DOTween.To(() => _material.GetColor("_MiddleColor"), x => _material.SetColor("_MiddleColor", x), middle, fadeTime);
        DOTween.To(() => _material.GetColor("_HighColor"), x => _material.SetColor("_HighColor", x), high, fadeTime);
    }


}

[Serializable]
public class BackgroundColors
{
    public Color low;
    public Color middle;
    public Color high;
    public float fadeTime;
}
