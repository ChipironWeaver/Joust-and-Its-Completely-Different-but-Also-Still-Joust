using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceTypeBehavior : MonoBehaviour
{
    [SerializeField] private List<DeviceTypeAction> _deviceTypes = new List<DeviceTypeAction>();
    private void Start()
    {
        foreach (DeviceTypeAction action in _deviceTypes)
        {
            if (action.deviceType == SystemInfo.deviceType)
            {
                foreach (Selectable selectable in action.selectablesToToggle)
                {
                    selectable.interactable = !selectable.interactable;
                }

                foreach (GameObject gameObjectToToggle in action.gameObjectsToToggle)
                {
                    gameObjectToToggle.SetActive(!gameObjectToToggle.activeSelf);
                }
            }
        }
    }
}

[Serializable]
public class DeviceTypeAction
{
    public DeviceType deviceType;
    public List<Selectable> selectablesToToggle = new List<Selectable>();
    public List<GameObject> gameObjectsToToggle = new List<GameObject>();
}