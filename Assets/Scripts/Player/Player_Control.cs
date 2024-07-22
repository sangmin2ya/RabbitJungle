using System.Collections;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Collections;


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
    private bool isDashing = false;
    private float dashDuration = 0.1f;
    private float dashTimer = 0f;
    private int dashCount;
    public float dashCoolTime = 0f;

    // Player Gun
    public GameObject[] playerGun;

    public float maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.isDead = false;
        // Max Health / Health Setting 
        maxHealth = DataManager.Instance.Health;
        healthUIManager.SethealthCount(maxHealth);
        //DataManager.Instance.Health = maxHealth;

        StartCoroutine("Flip");
        StartCoroutine("ChargeDash");
        StartCoroutine("HitDelay");

        DataManager.Instance.Speed = 10.0f;
        DataManager.Instance.DashCount = 2;

        dashCount = DataManager.Instance.DashCount;
        DataManager.Instance.DashState = false;
        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            StartCoroutine(Pause(3f));
        }
    }
    IEnumerator Pause(float time)
    {
        DataManager.Instance.isFreeze = true;
        yield return new WaitForSeconds(time);
        DataManager.Instance.isFreeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DataManager.Instance.isFreeze)
        {
            Move();
        }
        // Toogle Map
        toggleMap();
        // Special Weapon
        if (DataManager.Instance.classChage)
        {
            SpecialWeaponGet();
            DataManager.Instance.classChage = false;
        }
        WeaponChange();
        GameObject.Find("Battle_Ui").transform.Find("SkillCoolDown").gameObject.SetActive(DataManager.Instance.weaponList.Any(x => x.Item1 == DataManager.Instance.SpecialWeapon && x.Item2 == true));
        // Check Player Life
        //PlayerDeath();
        healthUIManager.SethealthCount(DataManager.Instance.Health);
    }
    private void Move()
    {
        // Player Movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        ChangeWeapon();
        Block();
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * DataManager.Instance.Speed);
        transform.Translate(Vector2.up * verticalInput * Time.deltaTime * DataManager.Instance.Speed);


        // Basic Movement Skill
        baseSkill();
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
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && !DataManager.Instance.DashState)
        {
            BaseSkill();
            //DataManager.Instance.DashCount--;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            DataManager.Instance.Speed = 10.0f;
        }

        if (isDashing)
        {
            Dash();
        }
    }

    // player movement skill
    private void BaseSkill()
    {
        if (dashCount > 0)
        {
            isDashing = true;
            DataManager.Instance.DashState = true;
            dashTimer = dashDuration;

            GameObject.Find("Canvas_Dash").transform.GetChild(dashCount).GetComponent<Image>().fillAmount = GameObject.Find("Canvas_Dash").transform.GetChild(dashCount + 1).GetComponent<Image>().fillAmount;
            GameObject.Find("Canvas_Dash").transform.GetChild(dashCount + 1).GetComponent<Image>().fillAmount = 0;
            dashCount--;
            StartCoroutine("DashCutter");
        }
    }

    void Dash()
    {
        dashTimer -= Time.deltaTime;

        if (dashTimer > 0)
        {
            transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * 40);
            transform.Translate(Vector2.up * verticalInput * Time.deltaTime * 40);
        }
        else
        {
            isDashing = false;
            //DataManager.Instance.Speed = DataManager.Instance.Speed;
        }
    }

    public void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (DataManager.Instance.weaponList.Any(x => x.Item1 == SpecialWeaponType.ShotGun.ToString()))
            {
                Debug.Log("무기변경1");
                DataManager.Instance.SpecialWeapon = SpecialWeaponType.ShotGun.ToString();
                DataManager.Instance.classChage = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (DataManager.Instance.weaponList.Any(x => x.Item1 == SpecialWeaponType.Rifle.ToString()))
            {
                Debug.Log("무기변경2");
                DataManager.Instance.SpecialWeapon = SpecialWeaponType.Rifle.ToString();
                DataManager.Instance.classChage = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (DataManager.Instance.weaponList.Any(x => x.Item1 == SpecialWeaponType.Sniper.ToString()))
            {
                Debug.Log("무기변경3");
                DataManager.Instance.SpecialWeapon = SpecialWeaponType.Sniper.ToString();
                DataManager.Instance.classChage = true;
            }
        }
    }
    public void WeaponChange()
    {
        if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Rifle.ToString())
        {
            playerGun[0].SetActive(false);
            playerGun[1].SetActive(true);
            playerGun[2].SetActive(false);
            playerGun[3].SetActive(false);
        }
        else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.ShotGun.ToString())
        {
            playerGun[0].SetActive(false);
            playerGun[1].SetActive(false);
            playerGun[2].SetActive(true);
            playerGun[3].SetActive(false);

        }
        else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Sniper.ToString())
        {
            playerGun[0].SetActive(false);
            playerGun[1].SetActive(false);
            playerGun[2].SetActive(false);
            playerGun[3].SetActive(true);
        }
    }

    // player gun switch case
    public void SpecialWeaponGet()
    {
        if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Rifle.ToString())
        {
            DataManager.Instance.firstMaxHealth = 4;
            DataManager.Instance.firstDashCount = 2;
            DataManager.Instance.firstSpeed = 10f;
            DataManager.Instance.firstDamage = 1;
            DataManager.Instance.firstAttackSpeed = 0.1f;
            DataManager.Instance.BulletCount = 50;
            DataManager.Instance.SkillDamage = DataManager.Instance.Damage + 1;
        }
        else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.ShotGun.ToString())
        {
            DataManager.Instance.firstMaxHealth = 4;
            DataManager.Instance.firstDashCount = 2;
            DataManager.Instance.firstSpeed = 10f;
            DataManager.Instance.firstDamage = 1;
            DataManager.Instance.firstAttackSpeed = 1;
            DataManager.Instance.BulletCount = 10;
            DataManager.Instance.SkillDamage = DataManager.Instance.Damage + 1f;

        }
        else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Sniper.ToString())
        {
            DataManager.Instance.firstMaxHealth = 4;
            DataManager.Instance.firstDashCount = 2;
            DataManager.Instance.firstSpeed = 10f;
            DataManager.Instance.firstDamage = 4;
            DataManager.Instance.firstAttackSpeed = 1;
            DataManager.Instance.BulletCount = 10;
            DataManager.Instance.SkillDamage = DataManager.Instance.Damage + 1f;
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
        DataManager.Instance.DashState = true;
        this.gameObject.layer = 10;
        yield return new WaitForSeconds(0.25f);
        DataManager.Instance.DashState = false;
        this.gameObject.layer = 0;
    }

    IEnumerator ChargeDash()
    {
        while (true)
        {
            yield return null;

            if (dashCount < DataManager.Instance.DashCount)
            {
                dashCoolTime += Time.deltaTime;
                GameObject.Find("Canvas_Dash").transform.GetChild(dashCount + 1).GetComponent<Image>().fillAmount = dashCoolTime / 3;
                if (dashCoolTime >= 3)
                {
                    dashCount++;
                    dashCoolTime = 0;
                }
            }
            else
                continue;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!DataManager.Instance.DashState)
        {
            if (!DataManager.Instance.beHit)
            {
                DataManager.Instance.beHit = true;
                Debug.Log("피해입음!");
                this.gameObject.GetComponent<HitEffect>().TriggerHitEffect();
                DataManager.Instance.Health = DataManager.Instance.Health - 0.5f;

                if (DataManager.Instance.Health <= 0)
                {
                    DataManager.Instance.beHit = false;
                    DataManager.Instance.isDead = true;
                }
            }
        }
    }

    IEnumerator HitDelay()
    {
        float hitDelay = 0f;

        while (true)
        {
            yield return null;

            if (DataManager.Instance.beHit)
            {
                hitDelay += Time.deltaTime;
                if (hitDelay >= 0.5f)
                {
                    hitDelay = 0f;
                    DataManager.Instance.beHit = false;
                }
            }
        }
    }
}
