using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossAroundBullet : MonoBehaviour
{
    private float bulletSpeed = 10.0f;
    private Transform playerTransform;
    private Vector2 bulletDirection;

    // Start is called before the first frame update
    void Start()
    {

        //몬스터 위치 기반으로 방향 설정
        bulletDirection = (playerTransform.position - transform.position).normalized;

    }

    void Update()
    {

    }



    // 탄막이 다른 콜라이더와 충돌 시 OnTriggerEnter2D 클래스 발생
    void OnCollisionEnter2D(Collision2D other)
    {
        // 탄막이 다른 콜라이더(other)와 충돌했을때 플레이어 태그인지 비교
        // and compare with Wall Tag
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); // 탄막 제거
        }
    }

}
