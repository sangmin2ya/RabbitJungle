using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class Gun_Basic_Shooting : MonoBehaviour
{

    public int ammo = 20;

    public GameObject bullet;
    public Transform spawnPos;

    public GameObject rotation;

    public float timeBetweenShots;
    public float shotTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && ammo > 0)
        {
            if(Time.time > shotTime)
            {
                Instantiate(bullet, spawnPos.position, rotation.transform.rotation );
                ammo--;
                shotTime = Time.time + timeBetweenShots;
            }

        }
        Reload();
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R)) {

            ammo = 20;

        }
    }
}
