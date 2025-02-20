using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5; // 배경 오브젝트 개수
    public int obstacleCount = 0; // 장애물  개수
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); // 씬에 있는 모든 오브젝트 중 Obstacle이 달려있는 오브젝트 찾기
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        // 랜덤 배치
        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    // BpLooper와 트리거 충돌한 오브젝트를 뒤로 보내는 메서드
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // "BackGround" 태그를 가진 오브젝트 재배치
        if (collision.CompareTag("BackGround"))
        {
            // 배경 오브젝트 가로 크기
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;

            // 현재 배경 오브젝트의 위치
            Vector3 pos = collision.transform.position;

            // 배경 오브젝트를 현재 위치에서 가로 길이 * numBgCount 만큼 오른쪽으로 이동시켜 무한 반복
            pos.x += widthOfBgObject * numBgCount;

            // 새로운 위치를 배경 오브젝트에 적용
            collision.transform.position = pos;
            return;
        }

        // 충돌한 오브젝트에 Obstacle 컴포넌트가 있는지 확인
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            // 새로운 위치에 랜덤하게 배치 및 마지막 위치 업데이트
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}