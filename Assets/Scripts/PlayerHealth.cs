using System;
using UnityEngine;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField,Tag] private string _enemyTag;
    
    private Rigidbody2D _rigidbody2D;
    private PlayerController _playerController;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
        _health = _maxHealth;
    }

    private void Death()
    {
        _health--;
        if (_health > 0)
        {
            Respawn();
            return;
        }
        PermaDeath();
    }

    private void Respawn()
    {
        Debug.Log("Respawn");
    }

    private void PermaDeath()
    {
        Debug.Log("PermaDeath");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_enemyTag))
        {
            AttackResult result = collision.gameObject.GetComponent<EnemyBehavior>().EnemyDuel(gameObject);
            switch (result)
            {
                case AttackResult.Bounce:
                    _rigidbody2D.linearVelocityX = -_playerController.velocityMemory.x * (_playerController.bounceForce / 100);
                    break;
                case AttackResult.EnemyDeath:
                    Debug.Log("kill");
                    break;
                case AttackResult.PlayerDeath:
                    Death();
                    break;
            }
        }
    }
}
