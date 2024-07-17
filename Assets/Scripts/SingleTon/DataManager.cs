using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 싱글턴 인스턴스
    private static DataManager instance;

    // Private fields for user stats
    private float speed;
    private float attacSpeed;
    private float health;
    private float damage;
    private int dashCount;
    private string weapon;
    private string specialWeapon;
    private int bulletCount;
    private float skillDamage;
    private float swordLength;
    private float shurikenDamage;
    private float axeDamage;
    public bool justCleared = false;
    public bool specialWeaponGet = false;   
    public bool epicSkill = false;
    
    // getset 에 접근하게 해주는 프로퍼티
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float AttacSpeed
    {
        get { return attacSpeed; }
        set { attacSpeed = value; }
    }

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int DashCount
    {
        get { return dashCount; }
        set { dashCount = value; }
    }

    public string Weapon
    {
        get { return weapon; }
        set { weapon = value; }
    }
    public string SpecialWeapon
    {
        get { return specialWeapon; }
        set { specialWeapon = value; }
    }
    public int BulletCount
    {
        get { return bulletCount; }
        set { bulletCount = value; }
    }
    public float SkillDamage
    {
        get { return skillDamage; }
        set { skillDamage = value; }
    }
    public float SwordLength
    {
        get { return swordLength; }
        set { swordLength = value; }
    }
    public float ShurikenDamage
    {
        get { return shurikenDamage; }
        set { shurikenDamage = value; }
    }
    public float AxeDamage
    {
        get { return axeDamage; }
        set { axeDamage = value; }
    }
    // Public property to access the singleton instance
    public static DataManager Instance
    {
        get
        {
            // If the instance is null, find or create the DataManager object
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();

                // 없다면 새로제작
                if (instance == null)
                {
                    GameObject obj = new GameObject("DataManager");
                    instance = obj.AddComponent<DataManager>();
                }
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    // Optional: Add any additional methods or functionality here

    private void Awake()
    {
        // Ensure that only one instance of DataManager exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
