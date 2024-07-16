using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_ShotHun : MonoBehaviour
{

    public int maxAmmo;
    public int ammo;
    public int ShootBulletCount;

    public GameObject bullet;
    public GameObject bulletEffect;
    public Transform spawnPos;

    public GameObject rotation;

    public float timeBetweenShots;
    public float shotTime;

    public bool isReloading;


    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && ammo > 0 && !isReloading)
        {
            if (Time.time > shotTime)
            {
                for (int i = -1; i < ShootBulletCount-1; i++)
                {
                    Quaternion rotate = Quaternion.Euler(0, 0, i);
                    Instantiate(bullet, spawnPos.position, rotation.transform.rotation * rotate);
                    
                }
                Instantiate(bulletEffect, spawnPos.position, rotation.transform.rotation);

                ammo--;
                shotTime = Time.time + timeBetweenShots;
            }

        }
        Reload();
    }

    private void Reload()
    {
        if (ammo == 0)
        {
            StartCoroutine("ReloadTime");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
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
