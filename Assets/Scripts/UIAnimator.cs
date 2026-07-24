using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    public bool isInAnimation;
    [SerializeField] private AnimationGroup[] _animationGroups;
    [SerializeField] private int _basePanelIndex = -1;
    [SerializeField] private bool _fadeBasePanelOnStart;

    private Stack<int> _backStackIndex = new Stack<int>();
    private List<int> _activeIndexList = new List<int>();
    private int _currentPanelIndex = -1;

    public void Start()
    {
        if (_fadeBasePanelOnStart)
        {
            Fade(_basePanelIndex);
        }
    }

    public void Fade(int baseIndex)
    {
        int index = Mathf.Abs(baseIndex);
        if(isInAnimation || _activeIndexList.Contains(index)) return;
        if(_animationGroups[index].soloOnScreen)
        {
            foreach (int fadeOutIndex in _activeIndexList)
            {
                _animationGroups[fadeOutIndex].Animate(true);
            }
            _activeIndexList.Clear();
        }
        _activeIndexList.Add(index);
        if(_currentPanelIndex != -1 && baseIndex > 0)
        {
            _backStackIndex.Push(_currentPanelIndex);
        }
        _currentPanelIndex = index;
        isInAnimation = true;
        StartCoroutine(ToggleIsInAnimation(_animationGroups[index].Animate()));
    }

    private IEnumerator ToggleIsInAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);
        isInAnimation = !isInAnimation;
    }
    [Button]
    public void Back()
    {
        if(isInAnimation) return;
        if(_backStackIndex.Count == 0) return;
        if (_animationGroups[_currentPanelIndex].soloOnScreen) Fade(_backStackIndex.Peek() * -1);
        _currentPanelIndex = _backStackIndex.Pop();
    }
    
    [Serializable]
    public class AnimationGroup
    {
        public bool soloOnScreen;
        public bool multipleObjects;
        public List<Image> buttonToDisable = new List<Image>();
        public AnimationObject singleAnimationObject;
        public AnimationObject[] animationObjects;
        public UnityEvent onAnimationStart;
        public float Animate(bool isFadeOut = false)
        {
            foreach (Image button in buttonToDisable)
            {
                button.raycastTarget = !isFadeOut;
            }
            float sequenceTime = 0;
            if (multipleObjects)
            {
                foreach (var animationObject in animationObjects)
                {
                    float time = animationObject.delay + animationObject.duration;
                    if(time > sequenceTime) sequenceTime = time;
                }
                foreach (var animationObject in animationObjects) animationObject.Animate(isFadeOut,sequenceTime);
            }
            else
            {
                sequenceTime = singleAnimationObject.delay + singleAnimationObject.duration;
                singleAnimationObject.Animate(isFadeOut);
            }
            onAnimationStart?.Invoke();
            return sequenceTime;
        }
    }
    
    [Serializable]
    public class AnimationObject
    {
        public RectTransform target;
        public bool useBaseTargetPosition = true;
        public Vector2 targetPosition;
        public Vector2 direction;
        public float distance;
        public Vector2 baseScale;
        public Ease easeType;
        public float duration;
        public float delay;
        public UnityEvent onAnimationStart;
        public UnityEvent onAnimationEnd;
        
        private Vector2 _basePosition;
        public void Animate(bool isFadeOut = false, float biggestDelay = 0)
        {
            _basePosition = target.localPosition;
            
            if (isFadeOut)
            {
                if(!target.gameObject.activeSelf) return;
                target.localPosition = useBaseTargetPosition ? target.localPosition : targetPosition;
            }
            else
            {
                target.gameObject.SetActive(true);
                target.localScale = baseScale;
                target.localPosition = distance * direction + (useBaseTargetPosition ? target.localPosition : targetPosition);
            }
            Sequence seq = DOTween.Sequence();
            seq.SetUpdate(true);
            seq.AppendInterval( isFadeOut ? Mathf.Max(biggestDelay - (delay + duration),0) : delay);
            seq.Append(target.DOLocalMove(isFadeOut ? distance * direction + (useBaseTargetPosition ? _basePosition : targetPosition): useBaseTargetPosition ? _basePosition  : targetPosition, duration).SetEase(easeType)); 
            seq.Join(target.DOScale(isFadeOut ? baseScale : Vector2.one, duration).SetEase(easeType));
            seq.JoinCallback(() => onAnimationStart?.Invoke());
            seq.OnComplete(() =>
            {
                if (isFadeOut)
                {
                    target.gameObject.SetActive(false);
                    target.localScale = Vector3.one;
                    target.localPosition = _basePosition;
                    onAnimationEnd?.Invoke();
                }
            });
        }
    }
}
