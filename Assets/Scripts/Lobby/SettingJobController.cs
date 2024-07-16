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
    // Start is called before the first frame update
    void Start()
    {

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
    public void SelectGun()
    {
        DataManager.Instance.Health = 4f;
        DataManager.Instance.Speed = 10f;
        DataManager.Instance.Damage = 2f;
        DataManager.Instance.DashCount = 2;
        DataManager.Instance.AttacSpeed = 0.25f;
        DataManager.Instance.Weapon = WeaponType.Gun.ToString();
        DataManager.Instance.BulletCount = 20;
        SceneManager.LoadScene("Game");
    }
    public void SelectSword()
    {
        DataManager.Instance.Health = 5f;
        DataManager.Instance.Speed = 10f;
        DataManager.Instance.Damage = 2f;
        DataManager.Instance.DashCount = 2;
        DataManager.Instance.AttacSpeed = 0.25f;
        DataManager.Instance.Weapon = WeaponType.Sword.ToString();
        DataManager.Instance.SwordLength = 2f;
        DataManager.Instance.AxeDamage = 5f;
        DataManager.Instance.ShurikenDamage = 2f;
        SceneManager.LoadScene("Game");
    }
}
