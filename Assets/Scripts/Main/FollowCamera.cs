using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Player; // 플레이어의 Transform 참조

    // 카메라가 추적 가능한 좌표 범위
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        // 타겟은 플레이어의 위치
        Vector3 targetPosition = Player.position;

        // 카메라의 위치를 min, max 사이로 제한
        float clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(targetPosition.y, minY, maxY);

        // 제한된 값을 적용하여 카메라 위치 설정
        transform.position = new Vector3(clampX, clampY, -10);
    }
}