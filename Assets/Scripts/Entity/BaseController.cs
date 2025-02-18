using System;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer;
    protected Vector2 moveDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return moveDirection; } }

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
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
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;
    }
}
