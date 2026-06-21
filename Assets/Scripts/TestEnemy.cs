using UnityEngine;

public class TestEnemy : EnemyBehavior
{
    [SerializeField] private AttackResult _result;
    public override AttackResult EnemyDuel(GameObject player)
    {
        return _result;
    }

    public override void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }
}
