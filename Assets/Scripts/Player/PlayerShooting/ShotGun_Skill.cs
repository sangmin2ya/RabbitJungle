using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun_Skill : MonoBehaviour
{
    public GameObject bigBullet;
    public GameObject bulletEffect;
    public Transform spawnPos;

    public GameObject rotation;
    public int ShootBulletCount;

    public float coolTime;
    public float time;

    public bool epicSkill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (epicSkill)
        {
            if (Input.GetMouseButton(1) && time > coolTime)
            {
                for (int i = 0; i < 2; i++)
                {

                    for (int j = 0; j < ShootBulletCount; j++)
                    {
                        int count = ShootBulletCount / 2;
                        Quaternion rotate = Quaternion.Euler(0, 0, j - count);
                        Instantiate(bigBullet, spawnPos.position, rotation.transform.rotation * rotate);
                    }

                }

                Instantiate(bulletEffect, spawnPos.position, rotation.transform.rotation);
                time = 0;
            }
            time = time + Time.deltaTime;
        }
            
    }
}
