using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHeat : MonoBehaviour
{
    private int enemyHP = 5;

    public ParticleSystem deathEffectPrefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    // 충돌 시 
    private void OnCollisionEnter2D(Collision2D collision)
    {

        /* for special weapon
                switch (DataManager.Instance.Weapon)
        {
            case nameof(WeaponType.Sword):
                Destroy(collision.gameObject);
                enemyHP -= 1;
                break;

            case nameof(WeaponType.Axe):
                Destroy(collision.gameObject);
                enemyHP -= 2;
                break;

            case nameof(WeaponType.Bow):
                Destroy(collision.gameObject);
                enemyHP -= 1;
                break;

            // 다른 무기 유형들...

            default:
                // for no weapon
                Debug.LogWarning("알 수 없는 무기 유형입니다.");
                break;
        }
          */



        // different damage according to weapon type ShortSword, LongSword, Axe, ShotGun, Rifle, Sniper
        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Skill"))
        {

            if (collision.gameObject.CompareTag("Weapon"))
            {
                if (DataManager.Instance.Weapon == WeaponType.Gun.ToString())
                {
                    Debug.Log("총 맞음!");
                    switch (DataManager.Instance.Weapon)
                    {
                        case nameof(SpecialWeaponType.ShotGun):
                            Destroy(collision.gameObject);
                            enemyHP -= 3;
                            Debug.LogWarning("샷건 맞음!");
                            break;

                        case nameof(SpecialWeaponType.Rifle):
                            Destroy(collision.gameObject);
                            Debug.LogWarning("라이플 맞음!");
                            enemyHP -= 2;
                            break;

                        case nameof(SpecialWeaponType.Sniper):
                            Destroy(collision.gameObject);
                            Debug.LogWarning("스나이퍼 맞음!");
                            enemyHP -= 5;
                            break;
                        default:
                            Destroy(collision.gameObject);
                            Debug.LogWarning("기본 총 맞음");
                            enemyHP -= 1;
                            break;
                    }
                }
                else if (DataManager.Instance.Weapon == WeaponType.Sword.ToString())
                {
                    Debug.Log("칼 맞음!");
                    switch (DataManager.Instance.Weapon)
                    {
                        case nameof(SpecialWeaponType.ShortSword):
                            Destroy(collision.gameObject);
                            enemyHP -= 2;
                            Debug.LogWarning("단검 맞음!");
                            break;

                        case nameof(SpecialWeaponType.LongSword):
                            Destroy(collision.gameObject);
                            Debug.LogWarning("대검 맞음!");
                            enemyHP -= 5;
                            break;

                        case nameof(SpecialWeaponType.Axe):
                            Destroy(collision.gameObject);
                            Debug.LogWarning("도끼 맞음!");
                            enemyHP -= 3;
                            break;
                        default:
                            Destroy(collision.gameObject);
                            Debug.LogWarning("기본 칼 맞음");
                            enemyHP -= 2;
                            break;
                    }
                }
                else
                {
                    Debug.Log("총기 타입 없음!");
                    enemyHP -= 2;
                }
            }
        }
        // when enemy heated, compare enemyHP
        //GameObject hp1 = transform.Find("HP_1").gameObject;



        Transform enemyHPTransform = gameObject.transform.Find("EnemyHP");
        Transform hp2Transform = enemyHPTransform.Find("HP_2");
        Transform hp3Transform = enemyHPTransform.Find("HP_3");
        Transform hp4Transform = enemyHPTransform.Find("HP_4");
        Transform hp5Transform = enemyHPTransform.Find("HP_5");
        switch (enemyHP)
        {
            case <= 0:
                Debug.Log("Die!");
                Destroy(gameObject);
                //Instantiate(deathEffectPrefab, transform.position, quaternion.identity); 사망 파티클

                break;

            case 1:
                Debug.Log("적피 1");
                if (hp2Transform != null)
                {
                    Destroy(hp2Transform.gameObject);
                }
                if (hp3Transform != null)
                {
                    Destroy(hp3Transform.gameObject);
                }
                if (hp4Transform != null)
                {
                    Destroy(hp4Transform.gameObject);
                }
                if (hp5Transform != null)
                {
                    Destroy(hp5Transform.gameObject);
                }
                //Destroy(enemyHPTransform.gameObject.transform.Find("HP_2"));
                break;

            case 2:
                Debug.Log("적피 2");
                if (hp3Transform != null)
                {
                    Destroy(hp3Transform.gameObject);
                }
                if (hp4Transform != null)
                {
                    Destroy(hp4Transform.gameObject);
                }
                if (hp5Transform != null)
                {
                    Destroy(hp5Transform.gameObject);
                }
                break;

            case 3:
                Debug.Log("적피 3");
                if (hp4Transform != null)
                {
                    Destroy(hp4Transform.gameObject);
                }
                if (hp5Transform != null)
                {
                    Destroy(hp5Transform.gameObject);
                }
                break;

            case 4:
                Debug.Log("적피 4");
                if (hp5Transform != null)
                {
                    Destroy(hp5Transform.gameObject);
                }
                break;

            default:
                break;
        }
    }
}

