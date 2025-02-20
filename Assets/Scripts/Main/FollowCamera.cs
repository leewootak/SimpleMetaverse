using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Player; // �÷��̾��� Transform�� �����մϴ�.

    // ī�޶� ����ٴ� �� �ִ� �ּ� �� �ִ� X ��ǥ
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        Vector3 targetPosition = Player.position;

        // ī�޶��� ��ġ�� min, max ���̷� ����
        float clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(targetPosition.y, minY, maxY);

        //transform.position = Vector3.Lerp(targetPosition);

        // ���ѵ� ���� �����Ͽ� ī�޶� ��ġ ����
        transform.position = new Vector3(clampX, clampY, -10);
    }
}