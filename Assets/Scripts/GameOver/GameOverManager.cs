using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public float zoomDuration = 1.0f;
    public float fadeDuration = 1.0f;
    public Image blackScreen;
    public string gameOverSceneName = "GameOver";
    // Update is called once per frame
    void Update()
    {
        if (DataManager.Instance.isDead)
        {
            EyeChange();
            blackScreen.gameObject.SetActive(true);
            StartCoroutine(GameOverRoutine());
        }
    }
    private void EyeChange()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetChild(3).gameObject.SetActive(true);
    }
    private IEnumerator GameOverRoutine()
    {
        // 초기 카메라 설정
        float startFieldOfView = mainCamera.m_Lens.OrthographicSize;
        float targetFieldOfView = 1.5f; // 줌인할 대상 값 (필요에 따라 조정 가능)

        // 타임스케일 초기화
        float startTimeScale = Time.timeScale;

        // 줌인과 타임스케일 조정
        for (float t = 0; t < zoomDuration; t += Time.deltaTime)
        {
            mainCamera.m_Lens.OrthographicSize = Mathf.Lerp(startFieldOfView, targetFieldOfView, t / zoomDuration);
            Time.timeScale = Mathf.Lerp(startTimeScale, 0.1f, t / zoomDuration);
            yield return null;
        }


        yield return new WaitForSecondsRealtime(0.5f);

        // 검은 화면 페이드 인
        if (blackScreen != null)
        {
            for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
            {
                Color color = blackScreen.color;
                color.a = Mathf.Lerp(0, 1, t / fadeDuration);
                blackScreen.color = color;
                yield return null;
            }

            // 마지막 값 설정
            Color finalColor = blackScreen.color;
            finalColor.a = 1;
            blackScreen.color = finalColor;
        }
        Time.timeScale = 1.0f;
        // 게임 오버 씬으로 전환
        SceneManager.LoadScene(gameOverSceneName);
    }
}
