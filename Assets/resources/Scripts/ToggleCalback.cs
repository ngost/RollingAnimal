using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCalback : MonoBehaviour
{
    BetterToggleGroup TglGroup;
    bool canChangeAble = false;

    Toggle low, medium, high;
    // Start is called before the first frame update
    void Start()
    {
        TglGroup = GameObject.Find("toggle_group").GetComponent<BetterToggleGroup>();
        TglGroup.OnChange += TglGroup_OnChange;
        StartCoroutine("delayor");
        //...
        low = GameObject.Find("low").GetComponent<Toggle>();
        medium = GameObject.Find("medium").GetComponent<Toggle>();
        high = GameObject.Find("high").GetComponent<Toggle>();

    }

    IEnumerator delayor()
    {
        yield return new WaitForSeconds(0.2f);
        canChangeAble = true;
    }

    private void OnDisable()
    {
        TglGroup.OnChange -= TglGroup_OnChange;
    }

    void TglGroup_OnChange(Toggle newActive)
    {
        if (canChangeAble)
        {
            switch (newActive.name)
            {
                case "low":
                    if (newActive.isOn && !medium.isOn && !high.isOn)
                    {
                        DataLoadAndSave.SaveGraphicQuality(0);
                        QualitySettings.SetQualityLevel(0, true);
                        Debug.Log("saved low");
                    }

                    break;
                case "medium":
                    if (newActive.isOn && !low.isOn && !high.isOn)
                    {
                        DataLoadAndSave.SaveGraphicQuality(1);
                        QualitySettings.SetQualityLevel(1, true);
                        Debug.Log("saved medium");
                    }

                    break;
                case "high":
                    if (newActive.isOn && !low.isOn && !medium.isOn)
                    {
                        DataLoadAndSave.SaveGraphicQuality(2);
                        QualitySettings.SetQualityLevel(2, true);
                        Debug.Log("saved high");
                    }
                    break;
            }
        }
        //            Debug.Log(string.Format("Toggle {0} selected", newActive.name));

    }
}
