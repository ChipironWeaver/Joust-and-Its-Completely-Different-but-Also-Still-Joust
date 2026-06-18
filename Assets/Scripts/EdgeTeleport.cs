using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EdgeTeleport : MonoBehaviour
{
    [SerializeField] private EdgeTeleport _linkedTeleport;
    [SerializeField] private int _tpDirection;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            if (MathF.Sign(collision.attachedRigidbody.linearVelocityX) == MathF.Sign(-_tpDirection))
            {
                collision.transform.position = new Vector3(_linkedTeleport.transform.position.x, collision.transform.position.y, collision.transform.position.z);
            }
        }
    }
}
