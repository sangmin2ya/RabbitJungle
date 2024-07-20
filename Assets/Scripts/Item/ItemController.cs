using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject normalCard;
    [SerializeField] private GameObject epicCard;
    [SerializeField] private GameObject CardSelectUI;
    private Button[] cardButtons = new Button[3];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        if (DataManager.Instance.justCleared)
        {
            DataManager.Instance.justCleared = false;
            StartCoroutine("CardSelect");
        }
    }
    IEnumerator CardSelect()
    {
        CardSelectUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        for (int i = 0; i < 3; i++)
        {
            GameObject card = RandcomCard();
            card.GetComponent<RectTransform>().position =
                new Vector3(card.GetComponent<RectTransform>().position.x + (i - 1) * 500,
                card.GetComponent<RectTransform>().position.y, card.GetComponent<RectTransform>().position.z);
            cardButtons[i] = card.GetComponent<Button>();
        }
        foreach (var button in cardButtons)
        {
            button.onClick.AddListener(() => OnCardSelected(button));
        }
    }
    private void OnCardSelected(Button selectedButton)
    {
        // 선택된 카드의 효과를 적용
        ApplyCardEffect(selectedButton.gameObject.name);

        // Time.timeScale을 1로 설정하여 게임 재개
        Time.timeScale = 1f;

        // 카드를 파괴
        foreach (var button in cardButtons)
        {
            Destroy(button.gameObject);
        }

        // 카드 선택 캔버스를 비활성화
        CardSelectUI.gameObject.SetActive(false);
    }
    private void ApplyCardEffect(string cardName)
    {
        switch (cardName)
        {
            case "card1":
                Debug.Log("에픽스킬!");
                for (int i = 0; i < DataManager.Instance.weaponList.Count; i++)
                {
                    if (DataManager.Instance.weaponList[i].Item2 == false)
                    {
                        DataManager.Instance.weaponList[i] = new System.Tuple<string, bool>(DataManager.Instance.weaponList[i].Item1, true);
                        break;
                    }
                }
                break;
            case "card2":
                DataManager.Instance.additionalDashCount += 1;
                Debug.Log("대시추가!");
                break;
            case "card3":
                Debug.Log("체력추가!");
                DataManager.Instance.additionalMaxHealth += 1;
                DataManager.Instance.Health += 1;
                break;
            case "card4":
                Debug.Log("사거리증가/도탄횟수증가!");
                if (DataManager.Instance.Weapon == WeaponType.Sword.ToString())
                    DataManager.Instance.SwordLength += 1;
                else
                    DataManager.Instance.bulletHp += 1;
                break;
            case "card5":
                Debug.Log("공격속도증가!");
                if (DataManager.Instance.Weapon == WeaponType.Gun.ToString())
                    DataManager.Instance.additionalAttackSpeed -= 0.02f;
                else
                    DataManager.Instance.additionalAttackSpeed += 10;
                break;
            case "card6":
                Debug.Log("이동속도증가!");
                DataManager.Instance.additionalSpeed += 1.5f;
                break;
            case "card7":
                Debug.Log("공격력증가!");
                DataManager.Instance.additionalDamage += 0.5f;
                break;
            case "card8":
                Debug.Log("체력회복!");
                if (DataManager.Instance.Health < DataManager.Instance.MaxHealth)
                    DataManager.Instance.Health += 0.5f;
                break;
            case "card9":
                Debug.Log("스킬쿨타임감소!");
                break;
            case "card10":
                Debug.Log("랜덤!");
                ApplyCardEffect("card" + Random.Range(6, 10));
                break;
            default:
                break;
        }
    }
    private void UpdateState()
    {
        DataManager.Instance.Damage = DataManager.Instance.firstDamage + DataManager.Instance.additionalDamage;
        DataManager.Instance.Speed = DataManager.Instance.firstSpeed + DataManager.Instance.additionalSpeed;
        DataManager.Instance.AttacSpeed = DataManager.Instance.firstAttackSpeed + DataManager.Instance.additionalAttackSpeed;
        DataManager.Instance.MaxHealth = DataManager.Instance.firstMaxHealth + DataManager.Instance.additionalMaxHealth;
        DataManager.Instance.DashCount = DataManager.Instance.firstDashCount + DataManager.Instance.additionalDashCount;
    }
    private GameObject RandcomCard()
    {
        GameObject card = null;
        int rand = Random.Range(1, 19);
        string weapon = null;
        //가장 먼저 먹었던 무기순서로 고유스킬 활성화 안된 무기 찾기
        for (int i = 0; i < DataManager.Instance.weaponList.Count; i++)
        {
            if (!DataManager.Instance.weaponList[i].Item2)
            {
                weapon = DataManager.Instance.weaponList[i].Item1;
                break;
            }
        }
        if (rand <= 2 && weapon != null)
        {
            string effect = "";
            if (weapon == SpecialWeaponType.ShortSword.ToString())
                effect = "나선수리검\n수리검 투척 갯수 증가";
            if (weapon == SpecialWeaponType.LongSword.ToString())
                effect = "회전회오리\n대검을 회전시킵니다.";
            if (weapon == SpecialWeaponType.Axe.ToString())
                effect = "잔혹한 도끼\n도끼투척 갯수 증가";
            if (weapon == SpecialWeaponType.ShotGun.ToString())
                effect = "[우클릭]\n스킬활성화\n벅 샷";
            if (weapon == SpecialWeaponType.Rifle.ToString())
                effect = "[우클릭]\n스킬활성화\n강화 사격";
            if (weapon == SpecialWeaponType.Sniper.ToString())
                effect = "[우클릭]\n스킬활성화\n관통 사격";

            card = Instantiate(epicCard, CardSelectUI.transform);
            card.gameObject.name = "card1";
            card.transform.Find("Explain").GetComponent<TextMeshProUGUI>().text = "힘의 차이가\n느껴집니다...";
            card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = effect;
        }
        else if (rand <= 5)
        {
            card = Instantiate(epicCard, CardSelectUI.transform);
            if (rand == 3)
            {
                card.gameObject.name = "card2";
                card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = "대쉬 갯수\n+ 1";
                card.transform.Find("Explain").GetComponent<TextMeshProUGUI>().text = "몸통박치기!";
            }
            else if (rand == 4)
            {
                card.gameObject.name = "card3";
                card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = "최대 체력 증가\n+ 1";
                card.transform.Find("Explain").GetComponent<TextMeshProUGUI>().text = "단단해지기!";
            }
            else if (rand == 5)
            {
                card.gameObject.name = "card4";
                if (DataManager.Instance.Weapon == WeaponType.Sword.ToString())
                {
                    card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = "사정거리 증가\n+ 1";
                    card.transform.Find("Explain").GetComponent<TextMeshProUGUI>().text = "어디까지 길어지는 거에요?";
                }
                else
                {
                    card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = "도탄 횟수 증가\n+ 1";
                    card.transform.Find("Explain").GetComponent<TextMeshProUGUI>().text = "이제 총알이 튕깁니다";
                }
            }
        }
        else
        {
            card = Instantiate(normalCard, CardSelectUI.transform);
            switch (rand % 6)
            {
                case 0:
                    card.gameObject.name = "card5";
                    card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text
                         = DataManager.Instance.Weapon == WeaponType.Gun.ToString() ? "사격 속도 증가\n- 0.02" : "칼 속도 증가\n+ 10";
                    card.transform.Find("Expain").GetComponent<TextMeshProUGUI>().text = "더 빠르게!";
                    break;
                case 1:
                    card.gameObject.name = "card6";
                    card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = "이동속도 증가\n+ 1.5";
                    card.transform.Find("Expain").GetComponent<TextMeshProUGUI>().text = "하지만 빨랐죠?";
                    break;
                case 2:
                    card.gameObject.name = "card7";
                    card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = "공격력 증가\n+ 0.5";
                    card.transform.Find("Expain").GetComponent<TextMeshProUGUI>().text = "힘이 곧 정의입니다.";
                    break;
                case 3:
                    card.gameObject.name = "card8";
                    card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = "체력 회복\n+ 1";
                    card.transform.Find("Expain").GetComponent<TextMeshProUGUI>().text = "죽을 것 같다면\n어쩔 수 없죠...";
                    break;
                case 4:
                    card.gameObject.name = "card9";
                    card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = "스킬 쿨타임 감소\n- 0.5";
                    card.transform.Find("Expain").GetComponent<TextMeshProUGUI>().text = "이미\n쿨타임이 0초는 아니죠?";
                    break;
                case 5:
                    card.gameObject.name = "card10";
                    card.transform.Find("Effect").GetComponent<TextMeshProUGUI>().text = "랜덤";
                    card.transform.Find("Expain").GetComponent<TextMeshProUGUI>().text = "말 그대로 랜덤입니다.";
                    break;
                default:
                    break;
            }
        }
        return card;
    }
}
