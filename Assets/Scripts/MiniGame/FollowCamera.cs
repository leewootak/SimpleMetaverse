using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    float offsetX;

    void Start()
    {
        if (target == null) return;

        offsetX = transform.position.x - target.position.x; // ó�� ī�޶� �����ǰ� Ÿ���� ������ �Ÿ� ����
    }

    /// <summary>
    /// transform.position.x�� �ٷ� �ٲٷ��� �ϸ� ������ ����� ������ ������ �����ߴٰ� ����ϴ� ���� �ʿ�
    /// </summary>
    void Update()
    {
        if (target == null) return;

        Vector3 pos = transform.position; // �� ��ġ ��������
        pos.x = target.position.x + offsetX; // ĳ���� ��ġ���� offsetX��ŭ �������� �̵�
        transform.position = pos;
    }
}
