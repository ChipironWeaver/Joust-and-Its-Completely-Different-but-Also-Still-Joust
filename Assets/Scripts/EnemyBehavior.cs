using System;
using UnityEngine;
using NaughtyAttributes;
[RequireComponent(typeof(Rigidbody2D)),RequireComponent(typeof(BoxCollider2D))]
public abstract class EnemyBehavior : MonoBehaviour
{
    public abstract AttackResult EnemyDuel(GameObject player);
    public abstract void Spawn();
    public abstract void Death();
    public bool isActive;

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
