using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using TMPro;
using UnityEngine.UI;


public class Rifle_Skill : MonoBehaviour
{
    public GameObject rifle;

    public float skillTime;
    private float skillCool;
    private float skillCoolTime;
    private float coolTIme;

    private bool skill = false;


    public GameObject[] CoolDownUI;
    public TextMeshProUGUI skillCoolDownText;

    // Start is called before the first frame update
    void Start()
    {
        skillCool = 10.0f;
        skillCoolTime = 10.0f;

        CoolDownUI = new GameObject[10];

        for (int i = 0; i < GameObject.Find("Battle_Ui").transform.Find("SkillCoolDown").transform.childCount; i++)
        {
            CoolDownUI[i] = GameObject.Find("Battle_Ui").transform.Find("SkillCoolDown").transform.GetChild(i).gameObject;
            if (CoolDownUI[i].name.Contains("Text"))
            {
                skillCoolDownText = CoolDownUI[i].transform.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        skillCoolTime = 10 - DataManager.Instance.additionalSkillCoolDown;
        if (DataManager.Instance.weaponList.Contains(new System.Tuple<string, bool>(SpecialWeaponType.Rifle.ToString(), true)))
        {
            if (!skill && skillCool > skillCoolTime)
            {
                if (Input.GetMouseButton(1))
                {
                    skill = true;
                    rifle.GetComponent<Gun_Rifle>().skill = skill;
                }
            }
            else if (skill)
            {
                skillTime = skillTime + Time.deltaTime;

                if (skillTime > 3)
                {
                    skill = false;
                    skillTime = 0;
                    rifle.GetComponent<Gun_Rifle>().skill = skill;
                    skillCool = 0;

                    for (int i = 0; i < CoolDownUI.Length; i++)
                    {
                        if (CoolDownUI[i] != null)
                        {
                            CoolDownUI[i].SetActive(true);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            if (skillCoolTime - skillCool > 0)
            {
                coolTIme = skillCoolTime - skillCool;
            }
            else if (skillCoolTime - skillCool < 0)
            {
                for (int i = 1; i < CoolDownUI.Length; i++)
                {
                    if (CoolDownUI[i] != null)
                    {
                        CoolDownUI[i].SetActive(false);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            skillCool = skillCool + Time.deltaTime;
            skillCoolDownText.text = coolTIme.ToString("0.0");
        }
    }

}

