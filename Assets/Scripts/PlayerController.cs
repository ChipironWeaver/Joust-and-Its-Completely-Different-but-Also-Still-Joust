using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float _maxSpeed;
    // Acceleration is calculated by: float% of _maxSpeed per seconde 
    [SerializeField] private float _groundAcceleration; 
    [SerializeField] private float _airAcceleration;
    [Header("Jump Settings")]
    [SerializeField] private float _groundJumpForce;
    [SerializeField] private float _fapForce;
    [Header("Ground Check Settings")]
    [SerializeField] private Vector2 _groundBoxSize;
    [SerializeField] private float _groundCastDistance;
    [SerializeField] private LayerMask _groundLayer;
    [Header("Wall Bounce Settings")]
    [SerializeField] private Vector2 _wallBoxSize;
    [SerializeField] private float _wallCastDistance;
    public float bounceForce;
    
    
    private float _horizontalInput;
    private float _horizontalInputMemory;
    [HideInInspector] public Vector2 velocityMemory;
    private bool _isGrounded;
    private bool _isSelfStoping;
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        _isGrounded = IsGrounded();
        
        if (MathF.Abs(_rigidbody2D.linearVelocityX) < 0.01 && velocityMemory.x != 0 && IsNextToWall())
            _rigidbody2D.linearVelocityX = -velocityMemory.x * (bounceForce / 100);
        
        else if (_rigidbody2D.linearVelocityX < _maxSpeed) 
            _rigidbody2D.linearVelocityX = Mathf.Clamp(_rigidbody2D.linearVelocityX +( _isGrounded ? _groundAcceleration : _airAcceleration) /100 
                * _horizontalInput * _maxSpeed * Time.fixedDeltaTime, -_maxSpeed, _maxSpeed);
        
        if (MathF.Abs(_rigidbody2D.linearVelocityY)  < 0.01 && velocityMemory.y > 0 && IsNextToWall())
            _rigidbody2D.linearVelocityY = -velocityMemory.y * (bounceForce / 100);

        if (_horizontalInput == 0 && _horizontalInputMemory != 0)
        {
            if (_isGrounded)
            {
                if (_horizontalInputMemory != MathF.Sign(_rigidbody2D.linearVelocityX))
                {
                    _isSelfStoping = true;
                }
                else
                {
                    _horizontalInputMemory = 0;
                    if(_isSelfStoping)
                    {
                        _isSelfStoping = false;
                        _rigidbody2D.linearVelocityX = 0;
                    }
                }
            }
            else
            {
                _isSelfStoping = false;
                _horizontalInputMemory = 0;
            }
        }
        if(_isSelfStoping)
        {
            _rigidbody2D.linearVelocityX += _groundAcceleration / 100 * _horizontalInputMemory * _maxSpeed * Time.fixedDeltaTime;
        }
        
        velocityMemory = _rigidbody2D.linearVelocity;
    }

    void OnMove(InputValue value)
    {
        _horizontalInput = value.Get<Vector2>().x;
        if (_horizontalInput != 0)
        {
            _horizontalInputMemory = _horizontalInput;
        }
    }

    void OnJump(InputValue value)
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
        Gizmos.color = Color.rebeccaPurple;
        Gizmos.DrawWireCube(transform.position - transform.up * _groundCastDistance, _groundBoxSize);
        Gizmos.color = Color.hotPink;
        Gizmos.DrawWireCube(transform.position + transform.up * _wallCastDistance, _wallBoxSize );
    }
}
