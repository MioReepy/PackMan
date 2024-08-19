using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _ghostSpeed;
    [SerializeField] private LayerMask _layerMask;
    private Vector2 initDirection;

    private float _siza = 0.75f;
    private float _angle = 0f;
    private float _distance = 1.5f;

    public Rigidbody2D RB { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 NextDirection { get; private set; }
    public Vector3 StartingPosition { get; private set; }
    
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        StartingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    private void ResetState()
    {
        _speed = 1f;
        Direction = initDirection;
        NextDirection = Vector2.zero;
        transform.position = StartingPosition;
        RB.isKinematic = false;
        enabled = true;
    }

    private void FixedUpdate()
    {
        Vector2 position = RB.position;
        Vector2 translation = Direction * (_speed * _ghostSpeed * Time.fixedDeltaTime);
        RB.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occuped(direction))
        {
            Direction = direction;
            NextDirection = Vector2.zero;
        }
        else
        {
            NextDirection = direction;
        }
    }

    public bool Occuped(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * _siza, _angle, direction, _distance);
        return hit.collider != null;
    }
}
