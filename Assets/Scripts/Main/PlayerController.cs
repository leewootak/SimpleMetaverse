using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer; // 캐릭터의 SpriteRenderer 컴포넌트 참조
    protected Vector2 moveDirection = Vector2.zero; // 현재 이동 방향
    public Vector2 MovementDirection { get { return moveDirection; } } // 외부에서 이동 방향 접근

    protected AnimationHandler animationHandler; // 애니메이션 처리 컴포넌트 참조

    protected Rigidbody2D rb; // 물리 연산을 위한 Rigidbody2D 컴포넌트 참조

    protected Vector2 rayDirection = Vector2.right; // 스캔을 위한 레이캐스트 방향

    [SerializeField] protected GameObject scanObject; // 레이캐스트로 탐지된 오브젝트
    [SerializeField] protected ScanManager scanManager; // 스캔 기능을 관리하는 ScanManager 컴포넌트 참조

    // 초기화
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Update()
    {
        // 사용자 입력을 처리하여 이동 및 스캔
        Action();

        // 이동 방향에 따라 캐릭터의 회전(좌우 반전)
        Rotate(moveDirection);

        // 스캔 중이 아닐 때만 스페이스바 입력으로 스캔 처리
        if (!scanManager.isScan && Input.GetButtonDown("Jump") && scanObject != null)
        {
            // 스캔 매니저를 통해 스캔
            scanManager.Scan(scanObject);
        }
    }

    protected virtual void Action()
    {
        // 스캔 중 이동 입력 무시, 그렇지 않으면 수평 및 수직 입력값 가져옴
        float horizontal = scanManager.isScan ? 0 : Input.GetAxisRaw("Horizontal");
        float vertical = scanManager.isScan ? 0 : Input.GetAxisRaw("Vertical");

        // 정규화하여 이동 속도 통일
        Vector2 inputDirection = new Vector2(horizontal, vertical).normalized;

        // 현재 입력값으로 진행하고 입력 없을 경우 멈춤
        moveDirection = inputDirection;

        // 입력에 따라 레이 방향 갱신
        if (inputDirection != Vector2.zero)
        {
            rayDirection = inputDirection;
        }
    }

    protected virtual void FixedUpdate()
    {
        // 현재 이동 방향에 따라 실제 이동 처리
        Movement(moveDirection);

        // 레이 시각화
        Debug.DrawRay(rb.position, rayDirection * 1.5f, new Color(0, 1, 0));

        // 설정한 레이어에 대해 스캔
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, rayDirection, 1.5f, LayerMask.GetMask("Object", "NPC"));

        // 레이와 충돌한 오브젝트가 있으면 scanObject에 할당, 없으면 null
        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }

    // 실제 이동 처리
    private void Movement(Vector2 direction)
    {
        // 이동 속도 조절
        direction = direction * 5;

        // Rigidbody2D의 속도 설정
        rb.velocity = direction;

        // 이동 애니메이션 갱신
        animationHandler.Move(direction);
    }

    // 방향 전환
    private void Rotate(Vector2 direction)
    {
        // 입력이 없으면 flip 상태 변경x
        if (direction == Vector2.zero)
            return;

        // 주어진 방향 벡터를 통해 회전 각도 계산 (radian을 degree로 변환)
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 회전 각도가 90도보다 크면 왼쪽 (절대값 사용)
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        // SpriteRenderer의 flipX 속성을 설정하여 좌우 반전 처리
        characterRenderer.flipX = isLeft;
    }
}
