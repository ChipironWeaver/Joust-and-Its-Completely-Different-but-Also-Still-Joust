using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    public abstract AttackResult EnemyDuel(GameObject player);
}

public enum AttackResult
{
    Bounce,
    PlayerDeath,
    EnemyDeath
}
