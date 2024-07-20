using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Control : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int bulletHp;
    private Rigidbody2D rb;
    private Vector2 incomingVector;
    [SerializeField] private GameObject bulletParticle;

    // Start is called before the first frame update
    void Start()
    {
        bulletHp = DataManager.Instance.bulletHp;
        Destroy(gameObject, lifeTime);
        rb = GetComponent<Rigidbody2D>();
        // 현재 총알의 속도 벡터
        Debug.Log("Incoming Vector: " + incomingVector);
    }

    private void LateUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (bulletHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        incomingVector = transform.rotation * Vector2.right;
        // 첫 번째 충돌 지점의 표면 노멀 벡터를 가져옴
        Vector2 normalVector = collision.contacts[0].normal;

        // 노멀 벡터를 디버깅 로그에 출력
        Debug.Log("Normal Vector: " + normalVector);

        // 반사 벡터 계산
        Vector2 reflectVector = Vector2.Reflect(incomingVector, normalVector);

        // 반사 벡터를 디버깅 로그에 출력
        Debug.Log("Reflect Vector: " + reflectVector);

        // 새로운 속도로 업데이트
        rb.velocity = reflectVector;

        // 총알의 방향을 반사된 방향으로 회전
        float angle = Mathf.Atan2(reflectVector.y, reflectVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bulletHp--;

        if (!collision.collider.CompareTag("Weapon") && !collision.collider.gameObject.CompareTag("Skill"))
        {
            Instantiate(bulletParticle, transform.position, Quaternion.identity);
        }
    }
}
