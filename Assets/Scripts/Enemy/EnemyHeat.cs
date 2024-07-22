using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHeat : MonoBehaviour
{
    private float enemyHP;
    private float enemyFullHp;

    public ParticleSystem dieParticle;
    public ParticleSystem cutParticle;
    public TextMeshPro damageText;


    // Start is called before the first frame update
    void Start()
    {
        enemyHP = 5.0f * (DataManager.Instance.StageLevel == 0 ? 1 : DataManager.Instance.StageLevel);
        enemyFullHp = enemyHP;
        //enemyHP = 5.0f * DataManager.Instance.StageLevel;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 충돌 시 
    private void OnTriggerEnter2D(Collider2D collision)
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
            StartCoroutine(HitEffect());
            Vector2 heatDirection = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            if (DataManager.Instance.Weapon == WeaponType.Sword.ToString())
            {
                cutParticle.Play();
                StartCoroutine(stopMove());
                GetComponent<Rigidbody2D>().AddForce((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).normalized * 2, ForceMode2D.Impulse);
            }
            else
            {
                StartCoroutine(stopMove());
                GetComponent<Rigidbody2D>().AddForce(heatDirection * 2, ForceMode2D.Impulse);
            }

            if (collision.gameObject.CompareTag("Weapon"))
            {
                if (DataManager.Instance.Weapon == WeaponType.Gun.ToString())
                {
                    Debug.Log("총 맞음!");
                    enemyHP -= DataManager.Instance.Damage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage);
                }
                else if (DataManager.Instance.Weapon == WeaponType.Sword.ToString())
                {
                    Debug.Log("칼 맞음!");
                    enemyHP -= DataManager.Instance.Damage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage);
                }
                else
                {
                    Debug.Log("총기 타입 없음!");
                    enemyHP -= DataManager.Instance.Damage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage);
                }
            }
            else if (collision.gameObject.CompareTag("Skill"))
            {
                Debug.Log("스킬 맞음!");
                if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Axe.ToString())
                {
                    Debug.Log("도끼 스킬 맞음!");
                    enemyHP -= DataManager.Instance.Damage + 2f;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage + 2f);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.LongSword.ToString())
                {
                    Debug.Log("대검 스킬 맞음!");
                    enemyHP -= DataManager.Instance.Damage * 2f;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage * 2f);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.ShortSword.ToString())
                {
                    Debug.Log("단검 스킬 맞음!");
                    enemyHP -= DataManager.Instance.Damage + 1;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage + 1);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Sniper.ToString())
                {
                    Debug.Log("저격 총알 맞음!");
                    enemyHP -= DataManager.Instance.SkillDamage * 1.5f;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.SkillDamage * 1.5f);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.ShotGun.ToString())
                {
                    Debug.Log("샷건 스킬 맞음!");
                    enemyHP -= DataManager.Instance.SkillDamage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.SkillDamage);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Rifle.ToString())
                {
                    Debug.Log("라이플 스킬 맞음!");
                    enemyHP -= DataManager.Instance.SkillDamage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.SkillDamage);
                }
                else
                {
                    Debug.Log("칼 기본 스킬 맞음!");
                    enemyHP -= DataManager.Instance.Damage * 1.5f;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage * 1.5f);
                }
            }
            else
            {
                Debug.Log("넌 왜 닳는거..?");
                enemyHP = DataManager.Instance.Damage;

            }
        }
        IEnumerator HitEffect()
        {
            transform.GetChild(1).Find("Head").gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            transform.GetChild(1).Find("Body").gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            transform.GetChild(1).Find("Ear1").gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            transform.GetChild(1).Find("Ear2").gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            yield return new WaitForSeconds(0.2f);

            transform.GetChild(1).Find("Head").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            transform.GetChild(1).Find("Body").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            transform.GetChild(1).Find("Ear1").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            transform.GetChild(1).Find("Ear2").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        // when enemy heated, compare enemyHP
        //GameObject hp1 = transform.Find("HP_1").gameObject;
        IEnumerator stopMove()
        {
            if (GetComponent<LongEnemyMovement>() != null)
            {
                GetComponent<LongEnemyMovement>().justHeat = true;
                yield return new WaitForSeconds(0.8f);
                GetComponent<LongEnemyMovement>().justHeat = false;
            }
            if (GetComponent<ShortEnemyMovement>() != null)
            {
                GetComponent<ShortEnemyMovement>().justHeat = true;
                yield return new WaitForSeconds(0.8f);
                GetComponent<ShortEnemyMovement>().justHeat = false;
            }
        }


        Transform enemyHPTransform = gameObject.transform.Find("EnemyHP");
        Transform hp2Transform = enemyHPTransform.Find("HP_2");
        Transform hp3Transform = enemyHPTransform.Find("HP_3");
        Transform hp4Transform = enemyHPTransform.Find("HP_4");
        Transform hp5Transform = enemyHPTransform.Find("HP_5");
        switch (enemyHP / enemyFullHp * 10)
        {
            case <= 0:
                Debug.Log("Die!");
                Instantiate(dieParticle, transform.position, Quaternion.identity);
                DataManager.Instance.killedEnemy++;
                Destroy(gameObject);
                //Instantiate(deathEffectPrefab, transform.position, quaternion.identity); 사망 파티클

                break;

            case <= 2:
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

            case <= 4:
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

            case <= 6:
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

            case <= 8:
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

