using UnityEngine;

public class UITransferPosition : MonoBehaviour
{
    [SerializeField] private RectTransform _target;

    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        _target.anchorMin = _rectTransform.anchorMin;
        _target.anchorMax = _rectTransform.anchorMax;
    }
}
