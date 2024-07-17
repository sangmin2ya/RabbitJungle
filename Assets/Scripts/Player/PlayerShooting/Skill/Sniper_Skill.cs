using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class Sniper_Skill : MonoBehaviour
{
    public GameObject bigBullet;
    public GameObject bulletEffect;
    public Transform spawnPos;

    public GameObject rotation;

    public float skillCoolTime;
    public float skillCool;
    private float coolTIme;

    public bool epicSkill;

    public GameObject[] CoolDownUI;
    public TextMeshProUGUI skillCoolDownText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (epicSkill)
        {
            if (Input.GetMouseButton(1) && skillCool > skillCoolTime)
            {
                Instantiate(bigBullet, spawnPos.position, rotation.transform.rotation);
                Instantiate(bulletEffect, spawnPos.position, rotation.transform.rotation);
                skillCool = 0;
                for (int i = 0; i < CoolDownUI.Length; i++)
                {
                    CoolDownUI[i].SetActive(true);
                }
            }
            skillCool = skillCool + Time.deltaTime;


            if (skillCoolTime - skillCool > 0)
            {
                coolTIme = skillCoolTime - skillCool;
            }
            else if (skillCoolTime - skillCool < 0)
            {
                for (int i = 0; i < CoolDownUI.Length; i++)
                {
                    CoolDownUI[i].SetActive(false);
                }
            }

            skillCool = skillCool + Time.deltaTime;
            skillCoolDownText.text = coolTIme.ToString("0.0");


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }

}
