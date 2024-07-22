using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageViewer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowDamage(float damage)
    {
        StartCoroutine(PopupDamage(damage));
    }
    private IEnumerator PopupDamage(float damage)
    {
        GetComponent<TextMeshPro>().text = damage.ToString("0.0");
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, 1f, 0);
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / 0.5f);
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치로 설정
        transform.position = endPosition;
        transform.localScale = endScale;

        // 오브젝트 파괴
        Destroy(gameObject);
    }
}
