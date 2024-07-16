using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject shortEnemy;
    public GameObject LongEnemy;
    private Transform playerTransform;
    private int numberOfEnemy = 10; // 몬스터 생성 개수
    
    //맵 상에서 적 생성 반경
    private float minX = -10.0f;
    private float maxX = 10.0f;
    private float minY = -10.0f;
    private float maxY = 10.0f;
    private float minDistanceFromPlayer = 1.0f; // 플레이어와 최소 거리
    
    // Start is called before the first frame update
    void Start()
    {
        // 스테이지 시작 시 플레이어 위치 찾기
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        SpawnEnemys();
        Debug.Log("스폰 에너미");
        
        
    }

    //임의의 SpawnEnemy 메소드 선언
    public void SpawnEnemys() {
        int spawnedEnemys = 0;
        while (spawnedEnemys < numberOfEnemy) {
            // 몹 스폰 위치 생성
            Vector2 spawnPosition = new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY));
            //플레이어와의 거리 계산
            float distanceFromPlayer = Vector2.Distance(spawnPosition, playerTransform.position);


            // 플레이어와의 최소 거리보다 길다면 몹 생성. 지금은 단거리 몹만 생성하지만 나중에 중거리 몹과 섞는 로직 짜야할듯
            if (distanceFromPlayer > minDistanceFromPlayer) {
                Instantiate(shortEnemy, spawnPosition, Quaternion.identity); // 기본 회전값으로 생성
                spawnedEnemys++;
            }
        }
    }
}
