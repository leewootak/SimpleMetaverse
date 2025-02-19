using System;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer;
    protected Vector2 moveDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return moveDirection; } }

    protected AnimationHandler animationHandler;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        Action();
        Rotate(moveDirection);
    }

    protected virtual void Action()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        Movement(moveDirection);
    }

    private void Movement(Vector2 direction)
    {
        direction = direction * 5;

        rb.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        // 입력이 없으면 flip 상태 변경하지 않음
        if (direction == Vector2.zero)
            return;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;
    }
}