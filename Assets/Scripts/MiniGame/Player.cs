using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator = null;
    Rigidbody2D rb = null;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isFlap = false;

    public bool godMode = false;

    GameManager gameManager;

    void Start()
    {
        // Ŭ���� ������ ��ü�� ���� (������ �� �̹� Ŭ������ Awake���� this�� ��ü�� �����ص� �����̱� ������ �ٷ� ���� ����)
        gameManager = GameManager.Instance;

        animator = transform.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();

        // ����ó��
        if (animator == null)
        {
            Debug.LogError("Not Founded Animator");
        }

        if (rb == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
    }

    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                // �����̽��ٳ� ���콺�� ��Ŭ���� ����� ���. (0)�� ������� ��� ��ġ
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    // ���� �����
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        gameManager.RestartGame();
                    }
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    public void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = rb.velocity; // ���ӵ�
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        rb.velocity = velocity; // Vector3�� ����ü�� ���� �������⸸ �� ���̰� ���� ��ȯ�Ѱ� �ƴϱ� ������ ���� ���� �־���

        float angle = Mathf.Clamp((rb.velocity.y * 10f), -90, 90); // ȸ�� ���� ����
        float lerpAngle = Mathf.Lerp(rb.velocity.y, angle, Time.deltaTime * 5f); // ������ ����
        transform.rotation = Quaternion.Euler(0, 0, lerpAngle); // z�� ȸ�� ����
    }

    /// <summary>
    /// �浹 �޼���
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        animator.SetInteger("isDie", 1); // �ִϸ����Ϳ��� isDie ������ �Ķ���� 1�� ����
        gameManager.GameOver();
        isDead = true;
        deathCooldown = 1f;
    }
}