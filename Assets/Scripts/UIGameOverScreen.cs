using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverScreen : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Image _panel;
    [SerializeField] private Image _background;
    [SerializeField] private List<Image> _panels;
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

    private void Start()
    {
        Time.timeScale = 1;
        _panel.transform.localScale = _initialPanelSize;
        _panel.transform.localPosition = _panelDirection * _panelOffset;
        foreach (var button in _panels)
        {
            button.raycastTarget = false;
        }
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

        Sequence fadeInSequence = DOTween.Sequence();
        fadeInSequence.SetUpdate(true);
        fadeInSequence.Append(_background.DOColor(win ? _winBackgroundColor : _loseBackgroundColor, _fadeInDuration));
        fadeInSequence.Join(_panel.transform.DOScale(Vector3.one, _fadeInDuration));
        fadeInSequence.Join(_panel.rectTransform.DOLocalMove(Vector3.zero, _fadeInDuration));
    }
}
