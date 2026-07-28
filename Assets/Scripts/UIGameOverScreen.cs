using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class UIGameOverScreen : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Image _panel;
    [SerializeField] private Image _background;
    [SerializeField] private List<Image> _panels;
    [SerializeField] private List<Image> _panelsWithMaterial;
    [SerializeField] private TextMeshProUGUI _endText;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private List<TextMeshProUGUI> _coloredText;
    [Header("Animation Settings")]
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private Vector3 _initialPanelSize;
    [SerializeField] private float _panelOffset;
    [SerializeField] private Vector3 _panelDirection;
    [SerializeField] private Ease _panelEase;
    [Header("Visual Settings")]
    [SerializeField] private Sprite _winPanel;
    [SerializeField] private Sprite _losePanel;
    [SerializeField] private Sprite _winButton;
    [SerializeField] private Sprite _loseButton;
    [SerializeField] private string _winString;
    [SerializeField] private string _loseString;
    [ResizableTextArea]
    [SerializeField] private string _wavePrefix;
    [SerializeField] private Color _winTextColor;
    [SerializeField] private Color _loseTextColor;
    [SerializeField] private Color _winBackgroundColor;
    [SerializeField] private Color _loseBackgroundColor;
    [SerializeField] private Material _winMaterial;
    [SerializeField] private Material _loseMaterial;
    
    private void Start()
    {
        Time.timeScale = 1;
        _panel.transform.localScale = _initialPanelSize;
        _panel.transform.localPosition = _panelDirection * _panelOffset;
        foreach (var button in _panels)
        {
            button.raycastTarget = false;
        }
        StartCoroutine(TimeLoop());
    }
    
    public void OnEnable()
    {
        Actions.Lose += () => ShowGameOverScreen(false);
        Actions.Win += () => ShowGameOverScreen(true);
    }

    public void OnDisable()
    {
        Actions.Lose -= () => ShowGameOverScreen(false);
        Actions.Win -= () => ShowGameOverScreen(true);
    }
    
    private void ShowGameOverScreen(bool win)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        _panel.sprite = win ? _winPanel : _losePanel;
        foreach (var button in _panels)
        {
            button.sprite = win ? _winButton : _loseButton;
            button.raycastTarget = true;
        }
        _endText.text = win ? _winString : _loseString;
        foreach (var text in _coloredText)
        {
            text.color = win ? _winTextColor : _loseTextColor;
        }
        _waveText.text = _wavePrefix + WaveManager.Instance.currentWave;

        foreach (Image panel in _panelsWithMaterial)
        {
            panel.material = win ? _winMaterial : _loseMaterial;
        }
        
        _background.DOColor(win ? _winBackgroundColor : _loseBackgroundColor, _fadeInDuration);
        
        UIAnimator.Instance.Fade(2);
    }

    private IEnumerator TimeLoop()
    {
        while (true)
        {
            _winMaterial.SetFloat("_UnscaledTime",Time.unscaledTime);
            _loseMaterial.SetFloat("_UnscaledTime",Time.unscaledTime);
            yield return new WaitForNextFrameUnit();
        }
    }
}
