using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 10.0f;
    private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        //플레이어 위치 인식
        playerTransform = GameObject.FindWithTag("Player").transform;
        //플레이어 위치와 몬스터 위치 기반으로 방향 설정
        Vector2 bulletDirection = (playerTransform.position - transform.position).normalized;
        // 탄막 속도 설정
        GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
    }



    // 탄막이 다른 콜라이더와 충돌 시 OnTriggerEnter2D 클래스 발생
    void OnTriggerEnter2D(Collider2D other) {
        // 탄막이 다른 콜라이더(other)와 충돌했을때 플레이어 태그인지 비교
        if (other.CompareTag("Player")) {
            Destroy(gameObject); // 탄막 제거
        }
    }
}
