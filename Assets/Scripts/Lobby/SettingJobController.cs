using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingJobController : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject selectjobCanvas;
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private GameObject gunInfo;
    [SerializeField] private GameObject swordInfo;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameStart()
    {
        mainCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
        mainCanvas.SetActive(false);
        selectjobCanvas.SetActive(true);
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    public void SelectGun()
    {
        DataManager.Instance.isDead = false;
        DataManager.Instance.StageLevel = 0;
        DataManager.Instance.MaxHealth = 4f;
        DataManager.Instance.Health = DataManager.Instance.MaxHealth;
        DataManager.Instance.Speed = 10f;
        DataManager.Instance.Damage = 2f;
        DataManager.Instance.DashCount = 2;
        DataManager.Instance.DashState = false;
        DataManager.Instance.AttacSpeed = 0.25f;
        DataManager.Instance.Weapon = WeaponType.Gun.ToString();
        DataManager.Instance.SpecialWeapon = null;
        DataManager.Instance.BulletCount = 20;

        DataManager.Instance.firstDamage = DataManager.Instance.Damage;
        DataManager.Instance.firstMaxHealth = DataManager.Instance.MaxHealth;
        DataManager.Instance.firstSpeed = DataManager.Instance.Speed;
        DataManager.Instance.firstDashCount = DataManager.Instance.DashCount;
        DataManager.Instance.firstAttackSpeed = DataManager.Instance.AttacSpeed;
        DataManager.Instance.weaponList.Clear();

        DataManager.Instance.additionalDamage = 0;
        DataManager.Instance.additionalMaxHealth = 0;
        DataManager.Instance.additionalSpeed = 0;
        DataManager.Instance.additionalDashCount = 0;
        DataManager.Instance.additionalSkillCoolDown = 0;
        DataManager.Instance.additionalAttackSpeed = 0;

        SceneManager.LoadScene("Intro");
    }
    public void SelectSword()
    {
        DataManager.Instance.isDead = false;
        DataManager.Instance.StageLevel = 0;
        DataManager.Instance.MaxHealth = 5f;
        DataManager.Instance.Health = DataManager.Instance.MaxHealth;
        DataManager.Instance.Speed = 10f;
        DataManager.Instance.Damage = 2f;
        DataManager.Instance.DashCount = 2;
        DataManager.Instance.DashState = false;
        DataManager.Instance.AttacSpeed = 500f;
        DataManager.Instance.Weapon = WeaponType.Sword.ToString();
        DataManager.Instance.SpecialWeapon = null;
        DataManager.Instance.SwordLength = 1f;
        DataManager.Instance.AxeDamage = 5f;
        DataManager.Instance.ShurikenDamage = 2f;

        DataManager.Instance.firstDamage = DataManager.Instance.Damage;
        DataManager.Instance.firstMaxHealth = DataManager.Instance.MaxHealth;
        DataManager.Instance.firstSpeed = DataManager.Instance.Speed;
        DataManager.Instance.firstDashCount = DataManager.Instance.DashCount;
        DataManager.Instance.firstAttackSpeed = DataManager.Instance.AttacSpeed;
        DataManager.Instance.weaponList.Clear();

        DataManager.Instance.additionalDamage = 0;
        DataManager.Instance.additionalMaxHealth = 0;
        DataManager.Instance.additionalSpeed = 0;
        DataManager.Instance.additionalDashCount = 0;
        DataManager.Instance.additionalAttackSpeed = 0;
        DataManager.Instance.additionalSkillCoolDown = 0;

        SceneManager.LoadScene("Intro");
    }
    public void ShowGunInfo()
    {
        gunInfo.SetActive(true);
        StartCoroutine(ScaleObject(GameObject.Find("GunRabbit"), new Vector3(1.3f, 1.3f, 1.3f), 0.1f));
        swordInfo.SetActive(false);
    }
    IEnumerator ScaleObject(GameObject obj, Vector3 targetScale, float duration)
    {
        Vector3 originalScale = obj.transform.localScale;
        float currentTime = 0.0f;

        while (currentTime <= duration)
        {
            float t = currentTime / duration;
            obj.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            currentTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = targetScale;
    }

    public void ShowSwordInfo()
    {
        gunInfo.SetActive(false);
        StartCoroutine(ScaleObject(GameObject.Find("SwordRabbit"), new Vector3(1.3f, 1.3f, 1.3f), 0.1f));
        swordInfo.SetActive(true);
    }
    public void CloseInfo()
    {
        gunInfo.SetActive(false);
        swordInfo.SetActive(false);
        StartCoroutine(ScaleObject(GameObject.Find("SwordRabbit"), new Vector3(1f, 1f, 1f), 0.1f));
        StartCoroutine(ScaleObject(GameObject.Find("GunRabbit"), new Vector3(1f, 1f, 1f), 0.1f));
    }
}
