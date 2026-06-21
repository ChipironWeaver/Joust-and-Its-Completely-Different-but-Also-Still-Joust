using System;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    public abstract AttackResult EnemyDuel(GameObject player);

    private void OnEnable()
    {
        LevelManager.Instance.RegisterEnemies(this);
    }
}

public enum AttackResult
{
    Bounce,
    PlayerDeath,
    EnemyDeath
}
