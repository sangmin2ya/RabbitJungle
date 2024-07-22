using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortEnemyMovement : MonoBehaviour
{
    private float moveSpeed = 7.0f;
    private Transform playerTransform;
    public bool justHeat = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!justHeat)
        {
            // player 태그를 향해서 Enemy 오브젝트가 이동
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerTransform = player.transform;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }
}