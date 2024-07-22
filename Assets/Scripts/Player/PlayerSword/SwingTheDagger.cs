using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SwingTheDagger : MonoBehaviour
{
    public float swingSpeed = 500.0f;
    public GameObject swordObject;
    public GameObject yeolpacham;
    //GameObject powerSlash;
    float deltaAngle = 0;
    float deltaTime = 0;
    float firstCoolTime = 0.5f;
    float coolTime;
    float lifespan = 2.0f;
    float swingAngle = 90.0f;

    Queue<List<GameObject>> q = new Queue<List<GameObject>>();

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
        if (coolTime <= 0.5)
            coolTime = 0.5f;
        else
            coolTime = firstCoolTime - DataManager.Instance.additionalSkillCoolDown;

        if (Input.GetMouseButton(0) && DataManager.Instance.SpecialWeapon == "ShortSword")
        {
            //if(DataManager.Instance.firstClassChage)
            {
                DataManager.Instance.firstMaxHealth = 4;
                DataManager.Instance.firstSpeed = 11f;
                DataManager.Instance.firstDamage = 1.5f;
                DataManager.Instance.firstDashCount = 3;
                DataManager.Instance.firstAttackSpeed = 800f;

                //DataManager.Instance.firstClassChage = false;
            }

            if (!swordObject.activeSelf)
            {
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
        if (Input.GetMouseButtonDown(1) && (deltaTime == 0 || deltaTime >= coolTime) && DataManager.Instance.SpecialWeapon == "ShortSword")
        {
            deltaTime = 0;

            for (int i = 0; i < CoolDownUI.Length; i++)
            {
                if (CoolDownUI[i] == null)
                    break;
                CoolDownUI[i].SetActive(true);
            }

            StartCoroutine(PowerSlash(0f));
        }
    }

    IEnumerator Swing()
    {
        swingSpeed = DataManager.Instance.firstAttackSpeed + DataManager.Instance.additionalAttackSpeed;
        transform.parent.GetComponent<Weapon_Rotation_Sword>().whileSwing = true;
        while (deltaAngle < swingAngle)
        {
            yield return null;

            float delta = swingSpeed * Time.deltaTime;

            transform.Rotate(-Vector3.forward * delta);
            deltaAngle += delta;

            if (deltaAngle > swingAngle)
            {
                swordObject.SetActive(false);
                transform.parent.GetComponent<Weapon_Rotation_Sword>().whileSwing = false;
            }
        }
    }
    IEnumerator PowerSlash(float t)
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        List<GameObject> powerSlash = new List<GameObject>();
        if (DataManager.Instance.weaponList.Any(x => x.Item1 == SpecialWeaponType.ShortSword.ToString() && x.Item2 == true))
        {
            powerSlash.Add(Instantiate(yeolpacham, new Vector2(transform.position.x, transform.position.y) + direction.normalized * 2, rotation));
            powerSlash.Add(Instantiate(yeolpacham, new Vector2(transform.position.x, transform.position.y) + direction.normalized * 2, rotation));
            powerSlash.Add(Instantiate(yeolpacham, new Vector2(transform.position.x, transform.position.y) + direction.normalized * 2, rotation));
            powerSlash[1].transform.Rotate(0, 0, -45);
            powerSlash[2].transform.Rotate(0, 0, 45);

        }
        else
        {
            powerSlash.Add(Instantiate(yeolpacham, new Vector2(transform.position.x, transform.position.y) + direction.normalized * 2, rotation));
        }
        q.Enqueue(powerSlash);

        while (true)
        {
            yield return null;
            if (powerSlash[0].IsDestroyed())
            {
                q.Dequeue();
                deltaTime = 0;
                break;
            }
            for (int i = 0; i < powerSlash.Count; i++)
            {
                Vector3 dir3 = rotation.eulerAngles;
                Vector3 dir = new Vector3(Mathf.Cos(dir3.z * Mathf.Deg2Rad), Mathf.Sin(dir3.z * Mathf.Deg2Rad), 0);
                // Vector3 dir = powerSlash[i].transform.forward;

                if (i == 0)
                {
                    powerSlash[i].transform.position += dir.normalized * Time.deltaTime * 50;
                }
                if (i == 1)
                {
                    powerSlash[i].transform.position += (Quaternion.Euler(0, 0, -45) * dir).normalized * Time.deltaTime * 50;
                }
                if (i == 2)
                {
                    powerSlash[i].transform.position += (Quaternion.Euler(0, 0, 45) * dir).normalized * Time.deltaTime * 50;
                }
                deltaTime += Time.deltaTime;
                t += Time.deltaTime;

                skillCoolDownText.text = (coolTime - deltaTime).ToString("0.0");
            }
            if (deltaTime >= coolTime && CoolDownUI[0].activeSelf)
            {
                for (int i = 1; i < CoolDownUI.Length; i++)
                {
                    if (CoolDownUI[i] == null)
                        break;
                    CoolDownUI[i].SetActive(false);
                }
            }

            if (t >= lifespan)
            {
                foreach (GameObject go in q.Dequeue())
                {
                    Destroy(go);
                }
                break;
            }

        }
    }
}
