using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject shortEnemy;
    public GameObject LongEnemy;
    private int playerLayerMask;

    private Transform playerTransform;
    private int numberOfEnemy = 25; // 몬스터 생성 개수
    
    //맵 상에서 적 생성 반경
    private float minX = -15.0f;
    private float maxX = 15.0f;
    private float minY = -15.0f;
    private float maxY = 15.0f;
    private float minDistanceFromPlayer = 10.0f; // 플레이어와 최소 거리
    
    // Start is called before the first frame update
    void Start()
    {
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

            // Select Enemy type Random
            GameObject enemyToSpawn = Random.Range(0,2) == 0 ? shortEnemy : LongEnemy;

            // 몹 스폰 위치 선언
            Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(minX, maxX),
                                                transform.position.y + Random.Range(minY, maxY));

            //플레이어와의 거리 계산
            float distanceFromPlayer = Vector2.Distance(spawnPosition, playerTransform.position);


            // 플레이어와의 최소 거리보다 길다면 몹 생성. 지금은 단거리 몹만 생성하지만 나중에 중거리 몹과 섞는 로직 짜야할듯
            if (distanceFromPlayer > minDistanceFromPlayer) {
                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity); // 기본 회전값으로 생성
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

    if (other.gameObject.layer == playerLayerMask && gameObject.transform.parent.GetComponent<RoomData>().RoomType.ToString() == RoomType.Battle.ToString()) {
        // spawnEnemies
        SpawnEnemies();
        //set currentMap cleared
        gameObject.transform.parent.GetComponent<RoomData>().RoomType = RoomType.Cleared.ToString();
 
    }
    }
}