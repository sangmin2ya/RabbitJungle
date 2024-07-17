using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverRecordManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetComponent<TextMeshProUGUI>().text = "Records : " + DataManager.Instance.StageLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
