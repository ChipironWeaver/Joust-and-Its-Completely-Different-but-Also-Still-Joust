using System;
using UnityEngine;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField,Tag] private string _enemyTag;
    
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Death()
    {
        Debug.Log("I'm dead");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_enemyTag))
        {
            AttackResult result = collision.gameObject.GetComponent<EnemyBehavior>().EnemyDuel(gameObject);
            switch (result)
            {
                case AttackResult.Bounce:
                    Debug.Log("Bounce");
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
