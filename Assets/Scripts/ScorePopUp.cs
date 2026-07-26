using System;
using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class ScorePopUp : MonoBehaviour
{
    [SerializeField,CurveRange(0, 0, 1, 1, EColor.Red)] private AnimationCurve _yPositionCurve;
    [SerializeField,CurveRange(0, 0, 1, 1, EColor.Yellow)] private AnimationCurve _xSinStrengthCurve;
    [SerializeField] private float _yPosition;
    [SerializeField] private float _xSinStrength;
    [SerializeField] private float _xSinRate;
    [SerializeField] private float _animationTime;
    [SerializeField] private Vector2 _finalScale;
    [SerializeField] private Color _finalColor;
    private TextMeshPro _textMesh;
    private float _basePositionX;
    
    public void Initialize(int score)
    {// new Vector3(MathF.Sin(x * _xSinRate) * _xSinStrength * _xSinStrengthCurve.Evaluate(x) + _basePositionX ,transform.position.y,transform.position.z) 
        _basePositionX = transform.position.x;
        _textMesh = GetComponent<TextMeshPro>();
        _textMesh.text = score.ToString();
        transform.DOLocalMoveY(_yPosition + transform.position.y, _animationTime).SetEase(_yPositionCurve);
        DOTween.To(() => 0f
            , x => transform.position = 
                new Vector3(MathF.Sin(x * _xSinRate * 6f) * _xSinStrength * _xSinStrengthCurve.Evaluate(x) + _basePositionX ,transform.position.y,transform.position.z) 
            , 1f, _animationTime);
        transform.DOScale(_finalScale, _animationTime);
        _textMesh.DOColor(_finalColor, _animationTime).OnComplete((() => Destroy(gameObject)));
    }

    [Button]
    private void Test()
    {
        Initialize(100);
    }
}
