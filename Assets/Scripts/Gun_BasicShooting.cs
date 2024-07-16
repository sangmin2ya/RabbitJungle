using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class Gun_Basic_Shooting : MonoBehaviour
{
    public int maxAmmo;
    public int ammo;


    public GameObject bullet;
    public GameObject bulletEffect;
    public Transform spawnPos;

    public GameObject rotation;
    public int damage;

    public float timeBetweenShots;
    public float shotTime;

    public bool isReloading;

    public DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        dataManager.Damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && ammo > 0 && !isReloading)
        {
            if(Time.time > shotTime)
            {
                Instantiate(bullet, spawnPos.position, rotation.transform.rotation );
                Instantiate(bulletEffect, spawnPos.position, rotation.transform.rotation);

                ammo--;
                shotTime = Time.time + timeBetweenShots;
            }

        }
        Reload();
    }

    private void Reload()
    {
        if(ammo == 0)
        {
            StartCoroutine("ReloadTime");
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine("ReloadTime");
        }
    }

    IEnumerator ReloadTime()
    {
        isReloading = true;
        yield return new WaitForSeconds(1);
        ammo = maxAmmo;
        isReloading = false;
    }

}
