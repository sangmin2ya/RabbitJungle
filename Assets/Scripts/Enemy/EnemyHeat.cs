using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeat : MonoBehaviour
{
    private int enemyHP = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 충돌 시 
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Weapon")) {
            Destroy(gameObject);
        }
    }
}
