using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Gun_Rifle : MonoBehaviour
{
    public int ammo;


    public GameObject bullet;
    public GameObject bigBullet;
    public GameObject bulletEffect;
    public Transform spawnPos;

    public GameObject rotation;

    public float firstTimeBetweenShots;
    private float timeBetweenShots;
    public float shotTime;

    public bool isReloading;
    public bool skill;

    public BulletUIManager bulletUIManager;

    void OnEnable()
    {
        bulletUIManager.SetBulletCount(ammo);
    }
    // Start is called before the first frame update
    void Start()
    {
        ammo = DataManager.Instance.BulletCount;
        bulletUIManager.SetBulletCount(ammo);
        timeBetweenShots = firstTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenShots = firstTimeBetweenShots + DataManager.Instance.additionalAttackSpeed;
        if (timeBetweenShots < 0.1f)
        {
            timeBetweenShots = 0.1f;
        }
        if (Input.GetMouseButton(0) && ammo > 0 && !isReloading && !DataManager.Instance.isFreeze)
        {
            if (Time.time > shotTime)
            {
                if (!skill)
                {
                    Instantiate(bullet, spawnPos.position, rotation.transform.rotation);
                    Instantiate(bulletEffect, spawnPos.position, rotation.transform.rotation);
                    ammo = ammo - 1;
                    bulletUIManager.SetBulletCount(ammo);
                }
                else if (skill)
                {
                    Instantiate(bigBullet, spawnPos.position, rotation.transform.rotation);
                    Instantiate(bulletEffect, spawnPos.position, rotation.transform.rotation);
                }

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
        ammo = DataManager.Instance.BulletCount;
        bulletUIManager.SetBulletCount(ammo);
        isReloading = false;
    }

}
