using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // �ؽð����� �ִϸ����� �Ķ���� ����
    private static readonly int IsMoving = Animator.StringToHash("IsMove");

    // Animator ������Ʈ ����
    protected Animator animator;

    // �ʱ�ȭ
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        // ���޹��� ������ ũ�Ⱑ 0.5���� ū ��� "IsMove" �Ķ���� ����
        animator.SetBool(IsMoving, obj.magnitude > 0.5f);
    }
}
