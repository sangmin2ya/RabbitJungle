using System.Collections;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player_Control : MonoBehaviour
{
    // Player Movement
    public float horizontalInput;
    public float verticalInput;
    
    // Player UI
    public GameObject map;
    public GameObject keyGuide;
    public HealthUIManager healthUIManager;

    // Dash
    private bool dashState = false;

    // Player Gun
    public GameObject[] playerGun;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.Health = 4.0f;
        StartCoroutine("Flip");
        DataManager.Instance.Speed = 10.0f;
        DataManager.Instance.DashCount = 2;
        healthUIManager.SethealthCount(DataManager.Instance.Health);
    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * DataManager.Instance.Speed);
        transform.Translate(Vector2.up * verticalInput * Time.deltaTime * DataManager.Instance.Speed);
        Block();

        // Basic Movement Skill
        baseSkill();

        // Toogle Map
        toggleMap();

        // Special Weapon
        if (DataManager.Instance.specialWeaponGet)
        {
            SpecialWeaponGet();
        }

        // Check Player Life
        PlayerDeath();

    }

    //Toogle Map 
    private void toggleMap()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            map.SetActive(!map.activeSelf);
            keyGuide.SetActive(!keyGuide.activeSelf);
        }
    }

    // player move block 
    private void Block()
    {
        RaycastHit2D[] hitdown = Physics2D.RaycastAll(transform.position, Vector2.down);

        for (int i = 0; i < hitdown.Length; i++)
        {
            if (hitdown[i].transform != null)
            {
                if (hitdown[i].distance < 1 && hitdown[i].collider.CompareTag("Wall"))
                {
                    if (verticalInput < 0)
                    {
                        verticalInput = 0;
                    }
                }

            }
        }

        RaycastHit2D[] hitup = Physics2D.RaycastAll(transform.position, Vector2.up);
        for (int i = 0; i < hitup.Length; i++)
        {
            if (hitup[i].transform != null)
            {
                if (hitup[i].distance < 1 && hitup[i].collider.CompareTag("Wall"))
                {
                    if (verticalInput > 0)
                    {
                        verticalInput = 0;
                    }
                }
            }
        }

        RaycastHit2D[] hitleft = Physics2D.RaycastAll(transform.position, Vector2.left);
        for (int i = 0; i < hitleft.Length; i++)
        {
            if (hitleft[i].transform != null)
            {
                if (hitleft[i].distance < 0.5 && hitleft[i].collider.CompareTag("Wall"))
                {
                    if (horizontalInput < 0)
                    {
                        horizontalInput = 0;
                    }
                }

            }
        }

        RaycastHit2D[] hitright = Physics2D.RaycastAll(transform.position, Vector2.right);
        for (int i = 0; i < hitright.Length; i++)
        {
            if (hitright[i].transform != null)
            {
                if (hitright[i].distance < 0.5 && hitright[i].collider.CompareTag("Wall"))
                {
                    if (horizontalInput > 0)
                    {
                        horizontalInput = 0;
                    }
                }
            }
        }
    }

    // player Death
    private void PlayerDeath()
    {
        if (DataManager.Instance.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // player movement skill
    public void baseSkill()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashState == false)
        {
            BaseSkill();
            DataManager.Instance.DashCount--;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            DataManager.Instance.Speed = 10.0f;
        }
    }

    // player movement skill
    private void BaseSkill()
    {
        DataManager.Instance.Speed = 40;
        StartCoroutine("DashCutter");
    }

    // player gun switch case
    public void SpecialWeaponGet()
    {
        if(DataManager.Instance.SpecialWeapon == SpecialWeaponType.Rifle.ToString())
        {
            playerGun[1].SetActive(true);
            playerGun[0].SetActive(false);
        }
        else if(DataManager.Instance.SpecialWeapon == SpecialWeaponType.ShotGun.ToString())
        {
            playerGun[2].SetActive(true);
            playerGun[0].SetActive(false);
        }
        else if(DataManager.Instance.SpecialWeapon == SpecialWeaponType.Sniper.ToString())
        {
            playerGun[3].SetActive(true);
            playerGun[0].SetActive(false);
        }
    }    

    IEnumerator Flip()
    {
        while (true)
        {
            yield return null;
            if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

        }
    }
 
    IEnumerator DashCutter()
    {
        dashState = true;
        this.gameObject.layer = 10;
        yield return new WaitForSeconds(0.25f);
        dashState = false;
        this.gameObject.layer = 0;
    }

    // oncollision update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("피해입음!");
            this.gameObject.GetComponent<HitEffect>().TriggerHitEffect();
            DataManager.Instance.Health = DataManager.Instance.Health - 0.5f;
            healthUIManager.SethealthCount(DataManager.Instance.Health);
        }
    }
}
