using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //[SerializeField] private GameObject player;

    private void Start()
    {

    }
    private void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            Debug.Log("지정된 오브젝트와 Door의 충돌 감지!");
            // 문과 연결된 장소로 이동
            transform.position = collision.gameObject.GetComponent<DoorData>().ConnectedDoorPosition;

        }
    }
}
