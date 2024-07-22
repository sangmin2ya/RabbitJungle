using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SwingTheLargeSword : MonoBehaviour
{
    public float swingSpeed = 500.0f;
    public GameObject swordObject;
    public GameObject yeolpacham;
    GameObject powerSlash;
    float deltaAngle = 0;
    float deltaTime = 0;
    float firstCoolTime = 5.0f;
    float coolTime;
    float lifespan = 2.0f;
    float swingAngle = 90.0f;
    bool firstClassChange = false;

    public GameObject[] CoolDownUI;
    public TextMeshProUGUI skillCoolDownText;

    // Start is called before the first frame update
    void Start()
    {
        CoolDownUI = new GameObject[10];
        coolTime = firstCoolTime;

        for (int i = 0; i < GameObject.Find("Battle_Ui").transform.Find("SkillCoolDown").transform.childCount; i++)
        {
            CoolDownUI[i] = GameObject.Find("Battle_Ui").transform.Find("SkillCoolDown").transform.GetChild(i).gameObject;
            if (CoolDownUI[i].name.Contains("Text"))
                skillCoolDownText = CoolDownUI[i].transform.GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (coolTime <= 1)
            coolTime = 1f;
        else
            coolTime = firstCoolTime - DataManager.Instance.additionalSkillCoolDown;

        if (Input.GetMouseButton(0) && DataManager.Instance.SpecialWeapon == "LongSword" && !DataManager.Instance.isFreeze)
        {
            //if (DataManager.Instance.firstClassChage)
            {
                DataManager.Instance.firstMaxHealth = 4;
                DataManager.Instance.firstSpeed = 8f;
                DataManager.Instance.firstDamage = 4f;
                DataManager.Instance.firstAttackSpeed = 300f;
                DataManager.Instance.firstDashCount = 2;

                //DataManager.Instance.firstClassChage = false;
            }

            if (!swordObject.activeSelf)
            {
                //swordObject.transform.localScale = new Vector3(swordObject.transform.localScale.x, DataManager.Instance.SwordLength, swordObject.transform.localScale.z);
                swordObject.SetActive(true);

                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //if (!transform.GetComponent<Weapon_Rotation_Sword>().whileSwing)
                transform.rotation = rotation;

                deltaAngle = 0;

                StartCoroutine("Swing");
            }
        }
        if (Input.GetMouseButtonDown(1) && (deltaTime == 0 || deltaTime >= coolTime) && DataManager.Instance.SpecialWeapon == "LongSword" && !DataManager.Instance.isFreeze)
        {
            deltaTime = 0;

            for (int i = 0; i < CoolDownUI.Length; i++)
            {
                if (CoolDownUI[i] == null)
                    break;
                CoolDownUI[i].SetActive(true);
            }

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            powerSlash = Instantiate(yeolpacham, new Vector2(transform.position.x, transform.position.y) + direction.normalized * 2, rotation);
            powerSlash.transform.localScale += new Vector3(1, 0.5f, 0);
            StartCoroutine(PowerSlash(rotation));
        }
    }

    IEnumerator Swing()
    {
        swingSpeed = DataManager.Instance.firstAttackSpeed + DataManager.Instance.additionalAttackSpeed;
        transform.parent.GetComponent<Weapon_Rotation_Sword>().whileSwing = true;
        float angle = DataManager.Instance.weaponList.Any(x => x.Item1 == SpecialWeaponType.LongSword.ToString() && x.Item2 == false) ? swingAngle : 360;
        float swing = DataManager.Instance.weaponList.Any(x => x.Item1 == SpecialWeaponType.LongSword.ToString() && x.Item2 == false) ? swingSpeed : (swingSpeed * 2);
        while (deltaAngle < angle)
        {
            yield return null;

            float delta = swing * Time.deltaTime;

            transform.Rotate(-Vector3.forward * delta);
            deltaAngle += delta;

            if (deltaAngle >= angle)
            {
                swordObject.SetActive(false);
                transform.parent.GetComponent<Weapon_Rotation_Sword>().whileSwing = false;
            }
        }
    }
    IEnumerator PowerSlash(Quaternion rot)
    {
        while (true)
        {
            yield return null;

            deltaTime += Time.deltaTime;

            skillCoolDownText.text = (coolTime - deltaTime).ToString("0.0");

            if (!powerSlash.IsDestroyed())
            {
                Vector3 dir3 = rot.eulerAngles;
                Vector3 dir = new Vector3(Mathf.Cos(dir3.z * Mathf.Deg2Rad), Mathf.Sin(dir3.z * Mathf.Deg2Rad), 0);
                powerSlash.transform.position += dir.normalized * Time.deltaTime * 50;

                if (deltaTime >= lifespan)
                    Destroy(powerSlash);
            }
            if (deltaTime >= coolTime)
            {
                for (int i = 1; i < CoolDownUI.Length; i++)
                {
                    if (CoolDownUI[i] == null)
                        break;
                    CoolDownUI[i].SetActive(false);
                }
                break;
            }

        }
        deltaTime = 0;
    }
}
