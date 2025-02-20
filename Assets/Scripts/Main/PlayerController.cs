using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer;
    protected Vector2 moveDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return moveDirection; } }

    protected AnimationHandler animationHandler;

    protected Rigidbody2D rb;

    protected Vector2 rayDirection = Vector2.right;

    [SerializeField] protected GameObject scanObject;
    [SerializeField] protected ScanManager scanManager;

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
        // 스캔 중이 아닐 때만 스페이스바 입력으로 스캔 처리
        if (!scanManager.isScan && Input.GetButtonDown("Jump") && scanObject != null)
        {
            scanManager.Scan(scanObject);
        }
    }

    protected virtual void Action()
    {
        float horizontal = scanManager.isScan ? 0 : Input.GetAxisRaw("Horizontal");
        float vertical = scanManager.isScan ? 0 : Input.GetAxisRaw("Vertical");
        Vector2 inputDirection = new Vector2(horizontal, vertical).normalized;

        // 이동은 현재 입력값으로 진행하여 입력 없으면 이동이 멈춤
        moveDirection = inputDirection;

        // 입력이 있으면 rayDirection 갱신
        if (inputDirection != Vector2.zero)
        {
            rayDirection = inputDirection;
        }
    }

    protected virtual void FixedUpdate()
    {
        Movement(moveDirection);

        // 스캐닝
        Debug.DrawRay(rb.position, rayDirection * 1.5f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, rayDirection, 1.5f, LayerMask.GetMask("Object", "NPC"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
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