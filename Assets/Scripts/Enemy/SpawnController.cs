using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject shortEnemy;
    public GameObject LongEnemy;
    public RoomData roomData; // 현재 룸 데이터 컴포넌트 참조
    private int playerLayerMask;

    private Transform playerTransform;
    private int numberOfEnemy = 10; // 몬스터 생성 개수
    
    //맵 상에서 적 생성 반경
    private float minX = -15.0f;
    private float maxX = 15.0f;
    private float minY = -15.0f;
    private float maxY = 15.0f;
    private float minDistanceFromPlayer = 2.0f; // 플레이어와 최소 거리
    
    // Start is called before the first frame update
    void Start()
    {
        roomData = GetComponent<RoomData>();
        if (roomData != null)
        {
            roomData.RoomType = "Battle"; // 방의 초기 상태 설정
        }
        else
        {
            Debug.LogError("RoomData 컴포넌트를 찾을 수 없습니다.");
        }
        roomData.RoomType = "Battle";
    }

    //임의의 SpawnEnemy 메소드 선언
    public void SpawnEnemies() {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player를 찾을 수 없어 몬스터를 스폰할 수 없습니다.");
            return;
        }
        Debug.Log("스폰 에너미");
        int spawnedEnemys = 0;
        while (spawnedEnemys < numberOfEnemy) {
            // 몹 스폰 위치 선언
            Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(minX, maxX),
                                                transform.position.y + Random.Range(minY, maxY));

            //플레이어와의 거리 계산
            float distanceFromPlayer = Vector2.Distance(spawnPosition, playerTransform.position);


            // 플레이어와의 최소 거리보다 길다면 몹 생성. 지금은 단거리 몹만 생성하지만 나중에 중거리 몹과 섞는 로직 짜야할듯
            if (distanceFromPlayer > minDistanceFromPlayer) {
                Instantiate(shortEnemy, spawnPosition, Quaternion.identity); // 기본 회전값으로 생성
                spawnedEnemys++;
            }
        }
    }
    private void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D other) {
    Debug.Log("콜리션 발동");
    
    // create layer mask 
    playerLayerMask = LayerMask.NameToLayer("Player");

    if (other.gameObject.layer == playerLayerMask && roomData.RoomType != "Cleared") {
        // spawnEnemies
        SpawnEnemies();
        //set currentMap cleared
        if (roomData != null) {
            // 방의 상태 변경
            roomData.RoomType = "Cleared";
            Debug.LogError("RoomData 컴포넌트를 찾을 수 없습니다.");
        }
    }
    }
}