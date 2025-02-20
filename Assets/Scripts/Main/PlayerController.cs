using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer; // ĳ������ SpriteRenderer ������Ʈ ����
    protected Vector2 moveDirection = Vector2.zero; // ���� �̵� ����
    public Vector2 MovementDirection { get { return moveDirection; } } // �ܺο��� �̵� ���� ����

    protected AnimationHandler animationHandler; // �ִϸ��̼� ó�� ������Ʈ ����

    protected Rigidbody2D rb; // ���� ������ ���� Rigidbody2D ������Ʈ ����

    protected Vector2 rayDirection = Vector2.right; // ��ĵ�� ���� ����ĳ��Ʈ ����

    [SerializeField] protected GameObject scanObject; // ����ĳ��Ʈ�� Ž���� ������Ʈ
    [SerializeField] protected ScanManager scanManager; // ��ĵ ����� �����ϴ� ScanManager ������Ʈ ����

    // �ʱ�ȭ
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Update()
    {
        // ����� �Է��� ó���Ͽ� �̵� �� ��ĵ
        Action();

        // �̵� ���⿡ ���� ĳ������ ȸ��(�¿� ����)
        Rotate(moveDirection);

        // ��ĵ ���� �ƴ� ���� �����̽��� �Է����� ��ĵ ó��
        if (!scanManager.isScan && Input.GetButtonDown("Jump") && scanObject != null)
        {
            // ��ĵ �Ŵ����� ���� ��ĵ
            scanManager.Scan(scanObject);
        }
    }

    protected virtual void Action()
    {
        // ��ĵ �� �̵� �Է� ����, �׷��� ������ ���� �� ���� �Է°� ������
        float horizontal = scanManager.isScan ? 0 : Input.GetAxisRaw("Horizontal");
        float vertical = scanManager.isScan ? 0 : Input.GetAxisRaw("Vertical");

        // ����ȭ�Ͽ� �̵� �ӵ� ����
        Vector2 inputDirection = new Vector2(horizontal, vertical).normalized;

        // ���� �Է°����� �����ϰ� �Է� ���� ��� ����
        moveDirection = inputDirection;

        // �Է¿� ���� ���� ���� ����
        if (inputDirection != Vector2.zero)
        {
            rayDirection = inputDirection;
        }
    }

    protected virtual void FixedUpdate()
    {
        // ���� �̵� ���⿡ ���� ���� �̵� ó��
        Movement(moveDirection);

        // ���� �ð�ȭ
        Debug.DrawRay(rb.position, rayDirection * 1.5f, new Color(0, 1, 0));

        // ������ ���̾ ���� ��ĵ
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, rayDirection, 1.5f, LayerMask.GetMask("Object", "NPC"));

        // ���̿� �浹�� ������Ʈ�� ������ scanObject�� �Ҵ�, ������ null
        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }

    // ���� �̵� ó��
    private void Movement(Vector2 direction)
    {
        // �̵� �ӵ� ����
        direction = direction * 5;

        // Rigidbody2D�� �ӵ� ����
        rb.velocity = direction;

        // �̵� �ִϸ��̼� ����
        animationHandler.Move(direction);
    }

    // ���� ��ȯ
    private void Rotate(Vector2 direction)
    {
        // �Է��� ������ flip ���� ����x
        if (direction == Vector2.zero)
            return;

        // �־��� ���� ���͸� ���� ȸ�� ���� ��� (radian�� degree�� ��ȯ)
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ȸ�� ������ 90������ ũ�� ���� (���밪 ���)
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        // SpriteRenderer�� flipX �Ӽ��� �����Ͽ� �¿� ���� ó��
        characterRenderer.flipX = isLeft;
    }
}
