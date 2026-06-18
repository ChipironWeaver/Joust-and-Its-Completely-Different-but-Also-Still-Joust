using UnityEngine;

public class TestEnemy : EnemyBehavior
{
    [SerializeField] private AttackResult _result;
    public override AttackResult EnemyDuel(GameObject player)
    {
        return _result;
    }
}
