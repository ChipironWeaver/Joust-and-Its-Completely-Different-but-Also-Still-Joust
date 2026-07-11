using UnityEngine;

public class EggBehavior : EnemyBehavior
{
    [SerializeField] private float _pushPower;
    [SerializeField] private int _pointAmount;
    [SerializeField] private float _enemySpawnTime;
    [SerializeField] private GameObject _enemyPrefab;
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
        _rigidbody2D.linearVelocity = direction * _pushPower;
        Invoke(nameof(SpawnEnemy) , _enemySpawnTime);
    }

    public void SpawnEnemy()
    {
        if (_enemyPrefab != null)
        {
            Instantiate(_enemyPrefab);
            Death();
        }
    }
    
    public override void Death()
    {
        isActive = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        //Animation
        Destroy(gameObject);
    }
}
