using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // 해시값으로 애니메이터 파라미터 제어
    private static readonly int IsMoving = Animator.StringToHash("IsMove");

    // Animator 컴포넌트 참조
    protected Animator animator;

    // 초기화
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        // 전달받은 벡터의 크기가 0.5보다 큰 경우 "IsMove" 파라미터 변경
        animator.SetBool(IsMoving, obj.magnitude > 0.5f);
    }
}
