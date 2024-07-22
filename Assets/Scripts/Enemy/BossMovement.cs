using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public GameObject bossShootBulletPrefab;
    public GameObject bossAroundBulletPrefab;
    private float moveSpeed = 4.0f;
    private float runspeed = 10.0f;
    private float bulletSpeed = 20.0f;
    private Transform playerTransform;

    private float bossShootFireRate = 0.5f;
    private float bossAroundFireRate = 1.0f;

    private enum BossState
    {
        Move,
        Rest,
        Run,
    }
    private enum BossShootState
    {
        Shoot,
        Stop,
        Around,
        Spread,
        Circle
    }

    private BossState currentState;
    private BossShootState currentShootState;
    void Awake()
    {
        //StartCoroutine(StopGameForSeconds());
    }
    IEnumerator StopGameForSeconds()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = BossState.Rest; // 처음 3초는 Rest
        StartCoroutine(StateControl());
        StartCoroutine(BossShootControl());
    }

    // Update is called once per frame
    /*void Update()
    {
        switch (currentState)
        {
            case BossState.Basic:
                break;
            case BossState.Shoot:
                BossShoot();
                break;
            case BossState.Move:
                BossMove();
                break;
            case BossState.Run:
                BossRun();
                break;
            case BossState.AroundShoot:
                BossAroundShoot();
                break;
        }
    }*/

    void Update()
    {
        switch (currentState)
        {
            case BossState.Move:
                BossMove();
                break;
            case BossState.Run:
                BossRun();
                break;
            case BossState.Rest:
                break;
        }
    }

    void BossMove()
    {
        // player 태그를 향해서 Enemy 오브젝트가 이동
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }
    void BossRun()
    {
        // player 태그를 향해서 Enemy 오브젝트가 이동
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, runspeed * Time.deltaTime);
    }

    void ShootBullet()
    {
        Instantiate(bossShootBulletPrefab, gameObject.transform.position, gameObject.transform.rotation); // 적 탄막 스폰 위치에서 적 탄막 생성
    }
    /*void AroundBullet()
    {
        int bulletCount = 10;
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;
            GameObject bullet = Instantiate(bossAroundBulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<BossAroundBullet>().SetBulletProperties(bulDir);
            Instantiate(bossAroundBulletPrefab, gameObject.transform.position, gameObject.transform.rotation);// 적 탄막 스폰 위치에서 적 탄막 생성

            angle += angleStep;
        }
    }
    */

    IEnumerator BossShootControl()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            CancelInvoke();
            BossShootState newState = (BossShootState)Random.Range(0, System.Enum.GetValues(typeof(BossShootState)).Length);
            currentShootState = newState;
            Debug.Log("슛 상태 변경!");
            switch (newState)
            {
                case BossShootState.Shoot:
                    InvokeRepeating("ShootBullet", 0f, bossShootFireRate);
                    break;
                case BossShootState.Around:
                    InvokeRepeating("ShootBullet", 0f, bossShootFireRate);
                    //InvokeRepeating("AroundBullet", 0f, bossAroundFireRate);
                    break;
                case BossShootState.Stop:
                    break;
                case BossShootState.Spread:
                    StartCoroutine(SpreadShot());
                    break;
                case BossShootState.Circle:
                    StartCoroutine(CircleShot());
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator CircleShot()
    {
        float angleStep = 360f / 20;
        for (float angle = 0; angle < 360; angle += angleStep)
        {
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            ShootBullet(direction);
        }
        yield return null;
    }
    IEnumerator RotateShot()
    {
        float angleStep = 360f / 10;
        for (float angle = 0; angle < 360; angle += angleStep)
        {
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            ShootBullet(direction);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator SpreadShot()
    {
        int numberOfBullets = 5;
        float spreadAngle = 30f;
        Vector3 directionToPlayer = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = baseAngle + (i - numberOfBullets / 2) * spreadAngle;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            ShootBullet(direction);
        }
        yield return null;
    }
    void ShootBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bossAroundBulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }
    IEnumerator StateControl()
    {
        //yield return new WaitForSeconds(3f); // Initial 3-second delay
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            // Randomly select a new state
            BossState newState = (BossState)Random.Range(0, System.Enum.GetValues(typeof(BossState)).Length);
            // Set the current state to the new random state
            currentState = newState;

            // Cancel any ongoing invokes when changing state

        }
    }

}