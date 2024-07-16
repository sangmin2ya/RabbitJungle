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
        if (enemyHP<=0) {
            Destroy(gameObject);
        }
    }

    // 충돌 시 
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        
        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Skill")) {

            if (DataManager.Instance.Weapon == WeaponType.Gun.ToString()) {
                Destroy(collision.gameObject);
                enemyHP -= 1;
            }
            else if (DataManager.Instance.Weapon == WeaponType.Sword.ToString()) {
                Destroy(collision.gameObject);
                enemyHP -= 3;
            }
            else {
                Destroy(collision.gameObject);
                enemyHP -= 2;
            }

        }
    }
}

