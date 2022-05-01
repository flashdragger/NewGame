using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int hour, min, sec;
    float CountTime;
    public int level=0;
    public Text TimeText;
    public bool isShowMlSec = true;
    // Start is called before the first frame update
    void Start()
    {
        Data.currentLevel = level;
        Data.setTime(0f);
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += Time.deltaTime;
        hour = (int)CountTime / 3600;
        min = (int)(CountTime - hour * 3600) / 60;
        sec = (int)(CountTime - hour * 3600 - min * 60);
        string msecStr = isShowMlSec ? ("." + ((int)((CountTime - (int)CountTime) * 10)).ToString("D1")) : "";
        TimeText.text = hour.ToString("D2") + ":" + min.ToString("D2") + ":" + sec.ToString("D2") + msecStr;

    }
}
