using UnityEngine;
using UnityEngine.Events;

public class EggBehavior : EnemyBehavior
{
    [SerializeField] private int _pointAmount;
    [SerializeField] private float _enemySpawnTime;
    [SerializeField] private UnityEvent _spawnEvent;
    private Rigidbody2D _rigidbody2D;
    public void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        LevelManager.Instance.RegisterEnemies(this);
        Spawn();
    }
    public override AttackResult EnemyDuel(GameObject player)
    {
        //Give points
        Death();
        return AttackResult.EnemyDeath;
    }

    public override void Spawn()
    {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        direction.Normalize();
        Invoke(nameof(SpawnEnemy) , _enemySpawnTime);
    }

    public void SpawnEnemy()
    {
        _spawnEvent.Invoke();
        Death();
    }
    
    public override void Death()
    {
        isActive = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        //Animation
        Destroy(gameObject);
    }
}
