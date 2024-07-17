using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    public GameObject Player_Sword;
    public GameObject Player_Gun;

    public CinemachineVirtualCamera characterCamera;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go;
        if (DataManager.Instance.Weapon == WeaponType.Gun.ToString())
        {
            go = Instantiate(Player_Gun, new Vector3(0, 0, 0), Player_Gun.transform.rotation);
        }
        else
        {
            go = Instantiate(Player_Sword, new Vector3(0, 0, 0), Player_Sword.transform.rotation);
        }
        characterCamera.GetComponent<CinemachineVirtualCamera>().Follow = go.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
