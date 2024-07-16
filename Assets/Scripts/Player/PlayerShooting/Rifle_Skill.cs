using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rifle_Skill : MonoBehaviour
{
    public GameObject rifle;

    public float skillTime;
    public float skillCool;

    private bool skill;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!skill && skillCool > 10)
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
            }
        }

        skillCool = skillCool + Time.deltaTime;

    }

}

