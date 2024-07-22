using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WeaponUIcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance.Weapon == WeaponType.Gun.ToString())
        {
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[1] 샷건";
            transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "[2] 돌격소총";
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "[3] 스나이퍼";
            transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "[1] 샷건";
            transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "[2] 돌격소총";
            transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>().text = "[3] 스나이퍼";
        }
        else if (DataManager.Instance.Weapon == WeaponType.Sword.ToString())
        {
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[1] 단검";
            transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "[2] 대검";
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "[3] 도끼";
            transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "[1] 단검";
            transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "[2] 대검";
            transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>().text = "[3] 도끼";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.Instance.weaponList.Any(x => x.Item1 == SpecialWeaponType.ShotGun.ToString() || x.Item1 == SpecialWeaponType.ShortSword.ToString()))
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
        if (DataManager.Instance.weaponList.Any(x => x.Item1 == SpecialWeaponType.Rifle.ToString() || x.Item1 == SpecialWeaponType.LongSword.ToString()))
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }
        if (DataManager.Instance.weaponList.Any(x => x.Item1 == SpecialWeaponType.Sniper.ToString() || x.Item1 == SpecialWeaponType.Axe.ToString()))
        {
            transform.GetChild(5).gameObject.SetActive(true);
        }
    }
}
