using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateMaterialTime : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    private void Start()
    {
        StartCoroutine(TimeLoop());
    }
    private IEnumerator TimeLoop()
    {
        while (true)
        {
            foreach(Material material in _materials) material.SetFloat("_UnscaledTime",Time.unscaledTime);
            yield return new WaitForNextFrameUnit();
        }
    }
}
