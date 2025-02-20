using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Player; // 플레이어의 Transform을 참조합니다.

    // 카메라가 따라다닐 수 있는 최소 및 최대 X 좌표
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        Vector3 targetPosition = Player.position;

        // 카메라의 위치를 min, max 사이로 제한
        float clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(targetPosition.y, minY, maxY);

        //transform.position = Vector3.Lerp(targetPosition);

        // 제한된 값을 적용하여 카메라 위치 설정
        transform.position = new Vector3(clampX, clampY, -10);
    }
}