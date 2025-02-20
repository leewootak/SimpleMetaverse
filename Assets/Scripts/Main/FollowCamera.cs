using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Player; // �÷��̾��� Transform ����

    // ī�޶� ���� ������ ��ǥ ����
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        // Ÿ���� �÷��̾��� ��ġ
        Vector3 targetPosition = Player.position;

        // ī�޶��� ��ġ�� min, max ���̷� ����
        float clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(targetPosition.y, minY, maxY);

        // ���ѵ� ���� �����Ͽ� ī�޶� ��ġ ����
        transform.position = new Vector3(clampX, clampY, -10);
    }
}