using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpecialJobController : MonoBehaviour
{
    public GameObject specialJobsUI;
    public List<UnityEngine.UI.Button> jobButtons = new List<UnityEngine.UI.Button>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && DataManager.Instance.weaponList.Count < 3)
        {
            JobSelect();
        }
    }
    private void JobSelect()
    {
        // Show 3 selectable card prefabs on screen
        specialJobsUI.SetActive(true);
        Time.timeScale = 0;
        // Change timescale to 0
        foreach (var button in jobButtons)
        {
            button.gameObject.SetActive(true);
            if (DataManager.Instance.weaponList.Any(x => x.Item1 == button.gameObject.name))
            {
                button.enabled = false;
                button.gameObject.transform.Find("Block").gameObject.SetActive(true);
            }
            else
                button.onClick.AddListener(() => OnJobSelected(button));
        }
    }
    private void OnJobSelected(UnityEngine.UI.Button selectedButton)
    {
        // Apply the effect of the selected card
        ApplyJobEffect(selectedButton.gameObject.name);
        // Resume the game
        Time.timeScale = 1f;
        // Destroy the card
        foreach (var button in jobButtons)
        {
            Destroy(button.gameObject);
        }
        // Disable the card selection canvas
        specialJobsUI.SetActive(false);
        // Destroy self
        Destroy(gameObject);
    }
    private void ApplyJobEffect(string jobName)
    {
        switch (jobName)
        {
            case "ShotGun":
                DataManager.Instance.SpecialWeapon = SpecialWeaponType.ShotGun.ToString();
                DataManager.Instance.weaponList.Add(new System.Tuple<string, bool>(SpecialWeaponType.ShotGun.ToString(), false));
                break;
            case "Rifle":
                DataManager.Instance.SpecialWeapon = SpecialWeaponType.Rifle.ToString();
                DataManager.Instance.weaponList.Add(new System.Tuple<string, bool>(SpecialWeaponType.Rifle.ToString(), false));
                break;
            case "Sniper":
                DataManager.Instance.SpecialWeapon = SpecialWeaponType.Sniper.ToString();
                DataManager.Instance.weaponList.Add(new System.Tuple<string, bool>(SpecialWeaponType.Sniper.ToString(), false));
                break;
            case "ShortSword":
                DataManager.Instance.SpecialWeapon = SpecialWeaponType.ShortSword.ToString();
                DataManager.Instance.weaponList.Add(new System.Tuple<string, bool>(SpecialWeaponType.ShortSword.ToString(), false));
                break;
            case "LongSword":
                DataManager.Instance.SpecialWeapon = SpecialWeaponType.LongSword.ToString();
                DataManager.Instance.weaponList.Add(new System.Tuple<string, bool>(SpecialWeaponType.LongSword.ToString(), false));
                break;
            case "Axe":
                DataManager.Instance.SpecialWeapon = SpecialWeaponType.Axe.ToString();
                DataManager.Instance.weaponList.Add(new System.Tuple<string, bool>(SpecialWeaponType.Axe.ToString(), false));
                break;
            default:
                break;
        }
        DataManager.Instance.classChage = true;
    }
}
