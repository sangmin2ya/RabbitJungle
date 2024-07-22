using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Gun_Basic_Shooting : MonoBehaviour
{
    public int ammo;


    public GameObject bullet;
    public GameObject bulletEffect;
    public Transform spawnPos;

    public GameObject rotation;

    public float firstTimeBetweenShots;
    private float timeBetweenShots;
    public float shotTime;

    public bool isReloading;


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
                Instantiate(bullet, spawnPos.position, rotation.transform.rotation);
                Instantiate(bulletEffect, spawnPos.position, rotation.transform.rotation);
                ammo = ammo - 1;
                shotTime = Time.time + timeBetweenShots;

                bulletUIManager.SetBulletCount(ammo);
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

    //public void AmmoText(int ammo)
    //{
    //    ammoText.text = "Ammo : " + ammo;
    //}

    IEnumerator ReloadTime()
    {
        isReloading = true;
        yield return new WaitForSeconds(1);
        ammo = DataManager.Instance.BulletCount;
        bulletUIManager.SetBulletCount(ammo);
        isReloading = false;
    }

}
