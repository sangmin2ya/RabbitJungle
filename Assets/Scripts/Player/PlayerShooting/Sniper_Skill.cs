using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper_Skill : MonoBehaviour
{
    public GameObject bigBullet;
    public GameObject bulletEffect;
    public Transform spawnPos;

    public GameObject rotation;

    public float coolTime;
    public float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && time > coolTime)
        {
            Instantiate(bigBullet, spawnPos.position, rotation.transform.rotation);
            Instantiate(bulletEffect, spawnPos.position, rotation.transform.rotation);
            time = 0;

        }
        time = time + Time.deltaTime;
    }
}
