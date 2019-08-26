using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTimer : MonoBehaviour
{

    bool courtineOnetimeFlag = false;
    GameObject timerObj;
    Text timerText;
    float times;
    bool stopFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        times = StaticInfoManager.lastTimerFloat;
        timerObj = GameObject.Find("Canvas").transform.Find("Timer").gameObject;
        timerText = timerObj.GetComponent<Text>();
        timerText.text = string.Format("{0:00} : {1:00}", (int)times / 60, (int)times % 60);
        StartCoroutine(TimeTextApply());
    }

    // Update is called once per frame
    void Update()
    {
        if (courtineOnetimeFlag && !stopFlag)
        {
            times = times + Time.deltaTime;
        }

    }

    public void TimerStopAndSave()
    {
        StaticInfoManager.lastTimerFloat = times;
        this.stopFlag = true;
    }

    IEnumerator TimeTextApply()
    {
        if (!courtineOnetimeFlag)
        {
            yield return new WaitForSeconds(1.5f);
            courtineOnetimeFlag = true;
            StartCoroutine(TimeTextApply());
        }
        else
        {
            timerText.text = string.Format("{0:00} : {1:00}", (int)times / 60, (int)times % 60);
            yield return new WaitForSeconds(1f);
            StartCoroutine(TimeTextApply());
        }


    }
}
