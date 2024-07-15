using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float moveSpeed = 1.0f;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed*Time.deltaTime);
        
    }
}