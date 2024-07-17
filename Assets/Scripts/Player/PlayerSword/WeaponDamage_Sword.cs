using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage_Sword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // get weapon type -> get weapon damage
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))// || collision.gameObject.CompareTag("Boss"))
        {
            // reduce enemy's hp or kill them
            //Destroy(collision.gameObject);
        }
    }
}
