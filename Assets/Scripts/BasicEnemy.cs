using System;
using UnityEngine;
using Random = UnityEngine.Random;
using NaughtyAttributes;

public class BasicEnemy : EnemyBehavior
{
    [Header("Speed Settings")]
    [SerializeField] private float _maxSpeed;
    // Acceleration is calculated by: float% of _maxSpeed per seconde 
    [SerializeField] private float _groundAcceleration; 
    [SerializeField] private float _airAcceleration;
    [Header("Jump Settings")]
    [SerializeField] private float _groundJumpForce;
    [SerializeField] private float _fapForce;
    [SerializeField,MinMaxSlider(0.0f, 1f)] private Vector2 _jumpCooldownRange;
    [SerializeField] private float _groundJumpCooldownMultiplier;
    [Header("Player Duel Settings")]
    [SerializeField] private float _enemyDeathHeight;
    [SerializeField] private float _playerDeathHeight;
    [Header("Ground Check Settings")]
    [SerializeField] private Vector2 _groundBoxSize;
    [SerializeField] private float _groundCastDistance;
    [SerializeField] private LayerMask _groundLayer;
    [Header("Wall Bounce Settings")]
    [SerializeField] private Vector2 _wallBoxSize;
    [SerializeField] private float _wallCastDistance;
    public float bounceForce;
    private float _horizontalInput;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private Vector2 _velocityMemory;
    private float _jumpCooldown;
    private float _currentJumpCooldown;
    
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _horizontalInput = Random.Range(0, 1) * 2 - 1;
        _jumpCooldown = 2;
        Spawn();
    }
    public override AttackResult EnemyDuel(GameObject player)
    {
        if (!isActive) return AttackResult.None;
        float heightDifference = player.transform.position.y - transform.position.y;
        print(heightDifference);
        if (heightDifference > _enemyDeathHeight)
        {
            Debug.Log("im dead - jone enemy");
            Death();
            return AttackResult.EnemyDeath;
        }
        HorizontalBounce();
        Jump();
        if (heightDifference < _playerDeathHeight)
        {
            Debug.Log("im dead - jone player");
            return AttackResult.PlayerDeath;
        }
        return AttackResult.Bounce;
    }

    public override void Death()
    {
        // ON DEATH EFFECT
        LevelManager.Instance.enemies.Remove(this);
        Destroy(gameObject);
    }

    public override void Spawn()
    {
        LevelManager.Instance.Spawn(gameObject);
        //play respawn animation + queue the activation
        isActive = true;
    }
    void FixedUpdate()
    {
        if (!isActive) return;
        _isGrounded = IsGrounded();

        _currentJumpCooldown += Time.fixedDeltaTime;
        if (_jumpCooldown * (_isGrounded ? _groundJumpCooldownMultiplier : 1) < _currentJumpCooldown)
        {
            _jumpCooldown = Random.Range(_jumpCooldownRange.x, _jumpCooldownRange.y);
            _currentJumpCooldown = 0;
            Jump();
        }
        if (MathF.Abs(_rigidbody2D.linearVelocityY)  < 0.01 && _velocityMemory.y > 0 && IsNextToWall())
            _rigidbody2D.linearVelocityY = -_velocityMemory.y * (bounceForce / 100);
        if (MathF.Abs(_rigidbody2D.linearVelocityX) < 0.01 && _velocityMemory.x != 0 && IsNextToWall())
            HorizontalBounce();
        else if (_rigidbody2D.linearVelocityX < _maxSpeed) 
            _rigidbody2D.linearVelocityX = Mathf.Clamp(_rigidbody2D.linearVelocityX +( _isGrounded ? _groundAcceleration : _airAcceleration) /100 
                * _horizontalInput * _maxSpeed * Time.fixedDeltaTime, -_maxSpeed, _maxSpeed);
        
        _velocityMemory = _rigidbody2D.linearVelocity;
    }

    public void HorizontalBounce()
    {
        _rigidbody2D.linearVelocityX = -_velocityMemory.x * (bounceForce / 100);
        _horizontalInput *= -1;
    }
    void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody2D.AddForce(Vector2.up * _groundJumpForce, ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody2D.AddForce(Vector2.up * _fapForce, ForceMode2D.Impulse);
        }
    }
    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, _groundBoxSize, 0, -transform.up, _groundCastDistance, _groundLayer))
        {
            return true;
        }
        return false;
    }
    private bool IsNextToWall()
    {
        if (Physics2D.BoxCast(transform.position, _wallBoxSize, 0, transform.up, _wallCastDistance, _groundLayer))
        {
            return true;
        }
        return false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.tomato;
        Gizmos.DrawWireCube(transform.position - transform.up * _groundCastDistance, _groundBoxSize);
        Gizmos.color = Color.chocolate;
        Gizmos.DrawWireCube(transform.position + transform.up * _wallCastDistance, _wallBoxSize );
    }
}
