using System;
using UnityEngine;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField,Tag] private string _enemyTag;
    [SerializeField] private HeartRenderer _renderer;
    
    public bool isActive;
    
    private Rigidbody2D _rigidbody2D;
    private PlayerController _playerController;

    private void Start()
    {
        LevelManager.Instance.RegisterPlayer(this);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
        _health = _maxHealth;
        _renderer.UpdateHearts(_health);
        Invoke( nameof(Respawn), 0.1f);
    }

    private void Death()
    {
        _health--;
        _renderer.UpdateHearts(_health);
        if (_health > 0)
        {
            Respawn();
            return;
        }
        PermaDeath();
    }

    private void Respawn()
    {
        Actions.PlayerRespawn?.Invoke();
        LevelManager.Instance.Spawn(gameObject);
        SetActivation(false);
        //Respawn stuff
        SetActivation(true);
    }

    [Button]
    private void PermaDeath()
    {
        Debug.Log("PermaDeath");
        Actions.PlayerDeath?.Invoke();
        LevelManager.Instance.RemovePlayer(this);
        SetActivation(false);
    }

    private void SetActivation(bool isSetActive)
    {
        isActive = isSetActive;
        _playerController.isActive = isSetActive;
        _rigidbody2D.constraints = !isActive?  RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
        if (!isSetActive) _rigidbody2D.linearVelocity = Vector2.zero;
        
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isActive) return;
        if (collision.gameObject.CompareTag(_enemyTag))
        {
            AttackResult result = collision.gameObject.GetComponent<EnemyBehavior>().EnemyDuel(gameObject);
            switch (result)
            {
                case AttackResult.Bounce:
                    _rigidbody2D.linearVelocityX = -_playerController.velocityMemory.x * (_playerController.bounceForce / 100);
                    break;
                case AttackResult.EnemyDeath:
                    break;
                case AttackResult.PlayerDeath:
                    Death();
                    break;
            }
        }
    }
}
