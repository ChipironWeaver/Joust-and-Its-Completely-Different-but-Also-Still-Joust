using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float _maxSpeed;
    // Acceleration is calculated by: float% of _maxSpeed per seconde 
    [SerializeField] private float _groundAcceleration; 
    [SerializeField] private float _airAcceleration;
    
    [Header("Ground Check Settings")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    
    
    
    private float _horizontalInput;
    private float _horizontalVelocity;
    
    
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        
    }

    void OnMove(InputValue value)
    {
        _horizontalInput = value.Get<Vector2>().x;
    }

    void OnJump(InputValue value)
    {
        
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
