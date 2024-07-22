using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    // Start is called before the first frame update
    void Start()
    {
        resultText.text = "처치한 악한 토끼 : <color=\"red\">" + DataManager.Instance.killedEnemy + "</color>\n플레이 타임 : <color=\"green\">" + FormatPlaytime(DataManager.Instance.playTime) + "</color>";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    string FormatPlaytime(float playtime)
    {
        int minutes = Mathf.FloorToInt(playtime / 60f);
        int seconds = Mathf.FloorToInt(playtime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
