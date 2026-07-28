using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputForUI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class RememberLastSelected : MonoBehaviour
{
    private EventSystem _eventSystem;
    private GameObject _lastSelectable;

    private void Start()
    {
        _eventSystem = EventSystem.current;
        
    }

    private void OnNavigate(InputValue value)
    {
        CheckForSelected();
    }

    private void OnCancel(InputValue value)
    {
        if(UIAnimator.Instance) UIAnimator.Instance.Back();
    }
    
    private void Update()
    {
        if (_eventSystem.currentSelectedGameObject) _lastSelectable = _eventSystem.currentSelectedGameObject;
    }

    private void CheckForSelected()
    {
        if (!_eventSystem.currentSelectedGameObject) _eventSystem.SetSelectedGameObject(_lastSelectable.gameObject);
    }
    
    
}
