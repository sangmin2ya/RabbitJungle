using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IntroGuideController : MonoBehaviour
{
    [SerializeField] private GameObject guidePanel;
    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private GameObject door;
    public int step = -1;
    private bool guideOn = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeStoryText());
        DataManager.Instance.isFreeze = true;
    }

    // Update is called once per frame
    void Update()
    {
        Skip();
        Guide();
    }
    private void Skip()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //StartCoroutine(FadeOutStory());
            DataManager.Instance.isFreeze = false;
            DataManager.Instance.StageLevel = 1;
            DataManager.Instance.Health = DataManager.Instance.MaxHealth;
            SceneManager.LoadScene("Game");
        }
    }
    private void Guide()
    {
        if (guideOn)
        {
            guideOn = false;
            switch (step)
            {
                case 0:
                    guidePanel.transform.Find("Move").gameObject.SetActive(true);
                    DataManager.Instance.isFreeze = false;
                    StartCoroutine(IncreaseStep(4f));
                    break;
                case 1:
                    // 2. 대쉬 방법
                    guidePanel.transform.Find("Move").gameObject.SetActive(false);
                    guidePanel.transform.Find("Dash").gameObject.SetActive(true);
                    StartCoroutine(ShowFocus(guidePanel.transform.Find("FocusDash").gameObject));
                    StartCoroutine(IncreaseStep(6f));
                    break;
                case 2:
                    guidePanel.transform.Find("Dash").gameObject.SetActive(false);
                    guidePanel.transform.Find("Attack").gameObject.SetActive(true);
                    StartCoroutine(ShowFocus(guidePanel.transform.Find(DataManager.Instance.Weapon == WeaponType.Sword.ToString() ? "FocusAttack" : "FocusGun").gameObject));
                    StartCoroutine(IncreaseStep(6.0f));
                    // 3. 공격 방법
                    break;
                case 3:
                    guidePanel.transform.Find("Attack").gameObject.SetActive(false);
                    guidePanel.transform.Find("Map").gameObject.SetActive(true);
                    StartCoroutine(IncreaseStep(7f));
                    // 4. 특수 무기 사용 방법
                    break;
                case 4:
                    GameObject.Find("Canvas").transform.Find("Map").gameObject.SetActive(false);
                    guidePanel.transform.Find("Map").gameObject.SetActive(false);
                    DataManager.Instance.isFreeze = true;
                    guidePanel.transform.Find("Upgrade").gameObject.SetActive(true);
                    StartCoroutine(IncreaseStep(6f));
                    break;
                case 5:
                    DataManager.Instance.isFreeze = false;
                    guidePanel.transform.Find("Upgrade").gameObject.SetActive(false);
                    StartCoroutine(IncreaseStep(5f));
                    break;
                case 6:
                    guidePanel.transform.Find("Start").gameObject.SetActive(true);
                    door.SetActive(true);
                    DataManager.Instance.Health = DataManager.Instance.MaxHealth;
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator IncreaseStep(float delay)
    {
        yield return new WaitForSeconds(delay);
        step++;
        guideOn = true;
    }
    IEnumerator ShowFocus(GameObject focus)
    {
        focus.SetActive(true);
        DataManager.Instance.isFreeze = true;
        yield return new WaitForSeconds(4f);
        focus.SetActive(false);
        DataManager.Instance.isFreeze = false;
        if (step == 2)
        {
            Instantiate(enemy, new Vector3(15, 0, 0), enemy.transform.rotation);
        }
    }
    private IEnumerator ChangeStoryText()
    {
        yield return StartCoroutine(ChangeTextWithFade("당신은 선한 토끼들의 용사입니다."));
        yield return StartCoroutine(ChangeTextWithFade("당신의 집이었던 정글은 악한 토끼들에게 침략당했습니다."));
        yield return StartCoroutine(ChangeTextWithFade("악한 토끼들을 모두 물리치고 고향을 되찾으세요."));
        storyText.gameObject.SetActive(false);

        StartCoroutine(FadeOutStory());
    }
    private IEnumerator FadeOutStory()
    {
        UnityEngine.UI.Image storyTextParent = storyText.transform.parent.GetComponent<UnityEngine.UI.Image>();
        float duration = 1.5f;
        float elapsedTime = 0f;
        float startOpacity = storyTextParent.color.a;
        float targetOpacity = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color tempColor = storyTextParent.color;
            tempColor.a = Mathf.Lerp(startOpacity, targetOpacity, t);
            storyTextParent.color = tempColor;
            yield return null;
        }
        storyTextParent.gameObject.SetActive(false);
        storyTextParent.color = new Color(storyTextParent.color.r, storyTextParent.color.g, storyTextParent.color.b, startOpacity);
        yield return new WaitForSeconds(1f);
        step = 0;
        guideOn = true;
    }
    private IEnumerator ChangeTextWithFade(string newText)
    {
        // 1초 동안 투명도를 0으로 변경
        yield return StartCoroutine(FadeTextToZeroAlpha(1f, storyText));
        // 텍스트 변경
        storyText.text = newText;
        // 1초 동안 투명도를 1로 변경
        yield return StartCoroutine(FadeTextToFullAlpha(1f, storyText));
        // 2초 유지
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    private IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
