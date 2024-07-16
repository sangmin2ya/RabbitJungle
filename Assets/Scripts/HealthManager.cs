using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject healthIconPrefeb;
    public Transform healthUiContainer;
    public float healthCount;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI(healthCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealthUI(float count)
    {
        foreach (Transform child in healthUiContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject bulletIcon = Instantiate(healthIconPrefeb, healthUiContainer);
        }
    }

    public void SethealthCount(int count)
    {
        healthCount = count;
        UpdateHealthUI(healthCount);
    }
}
