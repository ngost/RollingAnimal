using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LanguageConvertManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "RewardScene":
                GameObject.Find("MaxCombo").GetComponent<Text>().text = StaticInfoManager.lang.getString("MaxCombo");
                GameObject.Find("ClearPercent").GetComponent<Text>().text = StaticInfoManager.lang.getString("ClearPercent");
                GameObject.Find("TotalReward").GetComponent<Text>().text = StaticInfoManager.lang.getString("TotalReward");
                break;
            case "SettingScene":
                Text back_text = GameObject.Find("Label_Background").GetComponent<Text>();
                Text effect_text = GameObject.Find("Label_Effect").GetComponent<Text>();
                Text low_text = GameObject.Find("Label_low").GetComponent<Text>();
                Text medium_text = GameObject.Find("Label_medium").GetComponent<Text>();
                Text high_text = GameObject.Find("Label_high").GetComponent<Text>();
                Text grahpic_text = GameObject.Find("GraphicText").GetComponent<Text>();

                back_text.text = StaticInfoManager.lang.getString("BackgroundMusic");
                effect_text.text = StaticInfoManager.lang.getString("EffectMusic");
                low_text.text = StaticInfoManager.lang.getString("GraphicLow");
                medium_text.text = StaticInfoManager.lang.getString("GraphicMedium");
                high_text.text = StaticInfoManager.lang.getString("GraphicHigh");
                grahpic_text.text = StaticInfoManager.lang.getString("GraphicText");

                int fontSize = (int)Mathf.Round(Screen.height / 30f);
                back_text.fontSize = fontSize;
                effect_text.fontSize = fontSize;
                low_text.fontSize = fontSize;
                medium_text.fontSize = fontSize;
                high_text.fontSize = fontSize;
                grahpic_text.fontSize = fontSize;
                break;
            case "InventoryScene":

                break;
            default:
                break;
        }
    }
}
