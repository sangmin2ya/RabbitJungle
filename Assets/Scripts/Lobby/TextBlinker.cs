using System.Collections;
using UnityEngine;

public class BlinkingObject : MonoBehaviour
{
    private GameObject targetObject; // 깜빡이게 할 오브젝트
    private float blinkInterval = 1.0f; // 깜빡이는 간격 (초)

    private void Start()
    {
        targetObject = gameObject.transform.GetChild(0).gameObject; // 자식 오브젝트 중 첫 번째 오브젝트를 대상으로 설정
        // Coroutine 시작
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            // 현재 활성화 상태를 반전시킴
            targetObject.SetActive(!targetObject.activeSelf);
            yield return new WaitForSeconds(0.3f);
            targetObject.SetActive(!targetObject.activeSelf);

            // blinkInterval 동안 대기
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}