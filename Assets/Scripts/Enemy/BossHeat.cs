using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BossHeat : MonoBehaviour
{
    public Slider HpBarSlider;
    public TextMeshPro damageText;
    public float bossHP;
    private float maxHP;

    // Start is called before the first frame update
    void Start()
    {
        bossHP = 150.0f * DataManager.Instance.StageLevel;
        maxHP = 150.0f * DataManager.Instance.StageLevel;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();

    }

    IEnumerator HitEffect()
    {
        transform.GetChild(0).Find("Head").gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        transform.GetChild(0).Find("Body").gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        transform.GetChild(0).Find("Ear1").gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        transform.GetChild(0).Find("Ear2").gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        transform.GetChild(0).Find("Right EyeBrow").gameObject.SetActive(true);
        transform.GetChild(0).Find("Left EyeBrow").gameObject.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        transform.GetChild(0).Find("Head").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        transform.GetChild(0).Find("Body").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        transform.GetChild(0).Find("Ear1").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        transform.GetChild(0).Find("Ear2").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        transform.GetChild(0).Find("Right EyeBrow").gameObject.SetActive(false);
        transform.GetChild(0).Find("Left EyeBrow").gameObject.SetActive(false);
    }
    public void CheckHp() //*HP 갱신
    {
        if (HpBarSlider != null)
            HpBarSlider.value = (bossHP / maxHP);
        Debug.Log("적 체력 : " + bossHP / maxHP);
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



        // different damage according to weapon type
        /*
        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Skill"))
        {

            if (DataManager.Instance.Weapon == WeaponType.Gun.ToString())
            {
                Debug.Log("총 맞음!");

                bossHP -= 1;
            }
            else if (DataManager.Instance.Weapon == WeaponType.Sword.ToString())
            {
                Debug.Log("칼 맞음!");
                bossHP -= 3;
            }
            else
            {
                Debug.Log("총기 타입 없음!");
                bossHP -= 2;
            }

        }
        */
        /*
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
                            bossHP -= 3;
                            Debug.LogWarning("샷건 맞음!");
                            break;

                        case nameof(SpecialWeaponType.Rifle):
                            Destroy(collision.gameObject);
                            Debug.LogWarning("라이플 맞음!");
                            bossHP -= 2;
                            break;

                        case nameof(SpecialWeaponType.Sniper):
                            Destroy(collision.gameObject);
                            Debug.LogWarning("스나이퍼 맞음!");
                            bossHP -= 5;
                            break;
                        default:
                            Destroy(collision.gameObject);
                            Debug.LogWarning("기본 총 맞음");
                            bossHP -= 1;
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
                            bossHP -= 2;
                            Debug.LogWarning("단검 맞음!");
                            break;

                        case nameof(SpecialWeaponType.LongSword):
                            Destroy(collision.gameObject);
                            Debug.LogWarning("대검 맞음!");
                            bossHP -= 5;
                            break;

                        case nameof(SpecialWeaponType.Axe):
                            Destroy(collision.gameObject);
                            Debug.LogWarning("도끼 맞음!");
                            bossHP -= 3;
                            break;
                        default:
                            Destroy(collision.gameObject);
                            Debug.LogWarning("기본 칼 맞음");
                            bossHP -= 2;
                            break;
                    }
                }
                else
                {
                    Debug.Log("총기 타입 없음!");
                    bossHP -= 2;
                }
            }
            else if (collision.gameObject.CompareTag("Skill"))
            {
                Debug.Log("스킬 맞음!");
                bossHP -= 5;
            }
            else
            {
                Debug.Log("넌 왜 닳는거..?");
                bossHP = -1;

            }
        }
        */
        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Skill"))
        {
            HitEffect();
            if (collision.gameObject.CompareTag("Weapon"))
            {
                if (DataManager.Instance.Weapon == WeaponType.Gun.ToString())
                {
                    Debug.Log("총 맞음!");
                    bossHP -= DataManager.Instance.Damage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0) + new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage);
                }
                else if (DataManager.Instance.Weapon == WeaponType.Sword.ToString())
                {
                    Debug.Log("칼 맞음!");
                    bossHP -= DataManager.Instance.Damage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage);
                }
                else
                {
                    Debug.Log("총기 타입 없음!");
                    bossHP -= DataManager.Instance.Damage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage);
                }
            }
            else if (collision.gameObject.CompareTag("Skill"))
            {
                Debug.Log("스킬 맞음!");
                if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Axe.ToString())
                {
                    Debug.Log("도끼 스킬 맞음!");
                    bossHP -= DataManager.Instance.Damage + 2;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage + 2);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.LongSword.ToString())
                {
                    Debug.Log("대검 스킬 맞음!");
                    bossHP -= DataManager.Instance.Damage * 2f;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage * 2);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.ShortSword.ToString())
                {
                    Debug.Log("단검 스킬 맞음!");
                    bossHP -= DataManager.Instance.Damage + 1;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage + 1);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Sniper.ToString())
                {
                    Debug.Log("단검 스킬 맞음!");
                    bossHP -= DataManager.Instance.SkillDamage * 1.5f;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.SkillDamage * 1.5f);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.ShotGun.ToString())
                {
                    Debug.Log("단검 스킬 맞음!");
                    bossHP -= DataManager.Instance.SkillDamage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0) + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f), 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.SkillDamage);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Rifle.ToString())
                {
                    Debug.Log("단검 스킬 맞음!");
                    bossHP -= DataManager.Instance.SkillDamage;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.SkillDamage);
                }
                else
                {
                    Debug.Log("모르는 스킬 맞음!");
                    bossHP -= DataManager.Instance.Damage * 1.5f;
                    GameObject go = Instantiate(damageText, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).gameObject;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    go.gameObject.GetComponent<DamageViewer>().ShowDamage(DataManager.Instance.Damage * 1.5f);
                }
            }
            else
            {
                Debug.Log("넌 왜 닳는거..?");
                bossHP = DataManager.Instance.Damage;

            }
        }

        // when enemy heated, compare bossHP
        switch (bossHP)
        {
            case <= 0:
                Debug.Log("Die!");
                HpBarSlider.value = 0;
                HpBarSlider.transform.parent.gameObject.SetActive(false);
                Destroy(gameObject);
                //Instantiate(deathEffectPrefab, transform.position, quaternion.identity); 사망 파티클

                break;

            /*case 1 :
            Debug.Log("적피 1");
            if (hp2Transform != null ) {
                Destroy(hp2Transform.gameObject);
            }
            if (hp3Transform != null ) {
                Destroy(hp3Transform.gameObject);
            }
            if (hp4Transform != null ) {
                Destroy(hp4Transform.gameObject);
            }
            if (hp5Transform != null ) {
                Destroy(hp5Transform.gameObject);
            }
            //Destroy(enemyHPTransform.gameObject.transform.Find("HP_2"));
            break;

            case 2 :
            Debug.Log("적피 2");
            if (hp3Transform != null ) {
                Destroy(hp3Transform.gameObject);
            }
            if (hp4Transform != null ) {
                Destroy(hp4Transform.gameObject);
            }
            if (hp5Transform != null ) {
                Destroy(hp5Transform.gameObject);
            }
            break;

            case 3 :
            Debug.Log("적피 3");
            if (hp4Transform != null ) {
                Destroy(hp4Transform.gameObject);
            }
            if (hp5Transform != null ) {
                Destroy(hp5Transform.gameObject);
            }
            break;

            case 4 :
            Debug.Log("적피 4");
            if (hp5Transform != null ) {
                Destroy(hp5Transform.gameObject);
            }
            break;
            */
            default:
                break;
        }
    }
}

