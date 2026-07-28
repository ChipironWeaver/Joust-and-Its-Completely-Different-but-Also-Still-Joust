using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RememberLastSelected : MonoBehaviour
{
    private EventSystem _eventSystem;
    private GameObject _lastSelectable;

    private void Start()
    {
        _eventSystem = EventSystem.current;
    }

    private void Update()
    {
        if (!_eventSystem.currentSelectedGameObject) _eventSystem.SetSelectedGameObject(_lastSelectable.gameObject);
        else _lastSelectable = _eventSystem.currentSelectedGameObject;
    }
}
