using System;
using UnityEngine;
using NaughtyAttributes;
[RequireComponent(typeof(Rigidbody2D)),RequireComponent(typeof(BoxCollider2D))]
public abstract class EnemyBehavior : MonoBehaviour
{
    [Tag]
    public string hazardTag;
    public abstract AttackResult EnemyDuel(GameObject player);
    public abstract void Spawn();
    public abstract void Death();
    public bool isActive;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(hazardTag))
        {
            Death();
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(hazardTag))
        {
            Death();
        }
    }

    private void Start()
    {
        LevelManager.Instance.RegisterEnemies(this);
    }
}

public enum AttackResult
{
    Bounce,
    PlayerDeath,
    EnemyDeath,
    None
}
