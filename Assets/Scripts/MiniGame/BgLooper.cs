using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5; // ��� ������Ʈ ����
    public int obstacleCount = 0; // ��ֹ�  ����
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); // ���� �ִ� ��� ������Ʈ �� Obstacle�� �޷��ִ� ������Ʈ ã��
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        // ���� ��ġ
        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    // BpLooper�� Ʈ���� �浹�� ������Ʈ�� �ڷ� ������ �޼���
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // "BackGround" �±׸� ���� ������Ʈ ���ġ
        if (collision.CompareTag("BackGround"))
        {
            // ��� ������Ʈ ���� ũ��
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;

            // ���� ��� ������Ʈ�� ��ġ
            Vector3 pos = collision.transform.position;

            // ��� ������Ʈ�� ���� ��ġ���� ���� ���� * numBgCount ��ŭ ���������� �̵����� ���� �ݺ�
            pos.x += widthOfBgObject * numBgCount;

            // ���ο� ��ġ�� ��� ������Ʈ�� ����
            collision.transform.position = pos;
            return;
        }

        // �浹�� ������Ʈ�� Obstacle ������Ʈ�� �ִ��� Ȯ��
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            // ���ο� ��ġ�� �����ϰ� ��ġ �� ������ ��ġ ������Ʈ
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}