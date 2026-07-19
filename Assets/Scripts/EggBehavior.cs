using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EggBehavior : EnemyBehavior
{
    [SerializeField] private int _pointAmount;
    [SerializeField] private float _spawnInvulnerabilityTime;
    [SerializeField] private float _enemySpawnTime;
    [SerializeField] private UnityEvent _spawnEvent;
    private Rigidbody2D _rigidbody2D;
    private float _lifeTime;
    public void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Spawn();
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime >= _enemySpawnTime)
        {
            _spawnEvent.Invoke();
            _lifeTime = 0;
            Death();
        }
    }

    public override AttackResult EnemyDuel(GameObject player)
    {
        if(_lifeTime >= _spawnInvulnerabilityTime)
        {
            Death();
            return AttackResult.EnemyDeath;
            //Give points
        }
        return AttackResult.None;
    }

    public override void Spawn()
    {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        direction.Normalize();
    }
    
    public override void Death()
    {
        isActive = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        LevelManager.Instance.enemies.Remove(this);
        Actions.EnemyDeath?.Invoke();
        //Animation
        Destroy(gameObject);
    }
}
