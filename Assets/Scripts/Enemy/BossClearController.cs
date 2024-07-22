using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossClearController : MonoBehaviour
{
    public GameObject Boss;
    public TextMeshProUGUI clearMsg;
    private bool Cleared = false;
    private bool Fighting = true;

    private Transform playerTransform;
    private GameObject portalObject;
    private GameObject secretDoor;
    private GameObject EndPortal;

    //맵 상에서 적 생성 반경
    private float minX = -15.0f;
    private float maxX = 15.0f;
    private float minY = -15.0f;
    private float maxY = 15.0f;
    private float minDistanceFromPlayer = 15.0f; // 플레이어와 최소 거리

    int spawnedBoss = 1;


    // Start is called before the first frame update
    void Start()
    {
        // Find the Portal object in the parent
        portalObject = transform.parent.Find("Portal")?.gameObject;
        secretDoor = transform.parent.Find("SecretDoorEnter")?.gameObject;
        EndPortal = transform.parent.Find("EndingPortal")?.gameObject;

        if (portalObject == null)
        {
            Debug.LogWarning("Portal object not found in parent!");
        }
        else
        {
            portalObject.SetActive(false); // Ensure the portal is initially inactive
            secretDoor.SetActive(false);
            EndPortal.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Cleared == false && Fighting == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("BOSS");
            gameObject.transform.parent.GetComponent<RoomData>().RemainedEnemy = enemies.Length;
            if (enemies.Length <= 0)
            {
                StartCoroutine(FadeText());
                Cleared = true;

                if (DataManager.Instance.StageLevel == 4)
                {
                    EndPortal.SetActive(true);
                }
                else
                {
                    portalObject.SetActive(true);
                }

                secretDoor.SetActive(true);

            }
        }
    }
    IEnumerator FadeText()
    {
        // 텍스트 투명도 0에서 1로 서서히 증가
        yield return StartCoroutine(FadeTo(1f, 1f));
        // 텍스트 투명도 1로 유지
        yield return new WaitForSeconds(1);
        // 텍스트 투명도 1에서 0으로 서서히 감소
        yield return StartCoroutine(FadeTo(0f, 1f));
    }

    IEnumerator FadeTo(float targetAlpha, float duration)
    {
        clearMsg.gameObject.SetActive(true);
        Color color = clearMsg.color;
        float startAlpha = color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            color.a = newAlpha;
            clearMsg.color = color;
            yield return null;
        }

        // 최종 alpha 설정
        color.a = targetAlpha;
        clearMsg.color = color;
    }
    /*public void SpawnBoss()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        Debug.Log("스폰 보스");

        while (spawnedBoss < 1)
        {
            Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(minX, maxX),
                                                transform.position.y + Random.Range(minY, maxY));

            //플레이어와의 거리 계산
            float distanceFromPlayer = Vector2.Distance(spawnPosition, playerTransform.position);


            // 플레이어와의 최소 거리보다 길다면 몹 생성.
            if (distanceFromPlayer > minDistanceFromPlayer)
            {
                Instantiate(Boss, spawnPosition, Quaternion.identity); // 기본 회전값으로 생성
                spawnedBoss++;
                Debug.Log("보스 변수 추가");
            }
        }

    }*/
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("콜리션 발동");
        if (other.gameObject.CompareTag("Player") && Fighting == false)
        {
            //SpawnBoss();
            //Fighting = true;
            //Debug.Log("파이팅 트루");
        }
    }*/
}

