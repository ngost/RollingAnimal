using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuIconInitor : MonoBehaviour
{
    GameObject coinIcon;
    GameObject settingIcon;
    GameObject shopIcon;
    GameObject characterIcon;
    GameObject stageNameText;
    GameObject stageClearPercentText;
    GameObject rightArrow,leftArrow;
    // Start is called before the first frame update
    void Start()
    {
        //coin icon layout init
        coinIcon = transform.Find("CoinsBtn").gameObject;
        RectTransform coin_trans = coinIcon.GetComponent<RectTransform>();
        float Coin_Icon_width = Screen.width * 0.4f;
        float Coin_Icon_height = Coin_Icon_width * 0.28f;
        coin_trans.sizeDelta = new Vector2(Coin_Icon_width, Coin_Icon_height);
        coin_trans.anchoredPosition3D = new Vector3(Coin_Icon_width * -0.5f, Coin_Icon_height * -0.5f, 0f);
        coinIcon.transform.Find("coinText").GetComponent<Text>().fontSize = Mathf.RoundToInt(Screen.width* 0.05f);

        //setting icon layout init
        settingIcon = transform.Find("SettingBtn").gameObject;
        RectTransform setting_trans = settingIcon.GetComponent<RectTransform>();
        float Setting_Icon_width = Screen.width * 0.18f;
        float Setting_Icon_height = Coin_Icon_height;
        setting_trans.sizeDelta = new Vector2(Setting_Icon_width, Setting_Icon_height);
        setting_trans.anchoredPosition3D = new Vector3(Setting_Icon_width * 0.5f, Setting_Icon_height * -0.5f, 0f);

        //shop icon..
        shopIcon = transform.Find("ShopBtn").gameObject;
        RectTransform shop_trans = shopIcon.GetComponent<RectTransform>();
        float Shop_Icon_width = Screen.width * 0.18f;
        float Shop_Icon_height = Coin_Icon_height;
        shop_trans.sizeDelta = new Vector2(Shop_Icon_width, Shop_Icon_height);
        shop_trans.anchoredPosition3D = new Vector3(Shop_Icon_width * 0.5f + (Screen.width * 0.18f), Shop_Icon_height * -0.5f, 0f);

        //characterIcon ...
        characterIcon = transform.Find("CharacterBtn").gameObject;
        RectTransform character_trans = characterIcon.GetComponent<RectTransform>();
        float Character_Icon_width = Screen.width * 0.18f;
        float Character_Icon_height = Coin_Icon_height;
        character_trans.sizeDelta = new Vector2(Character_Icon_width, Character_Icon_height);
        character_trans.anchoredPosition3D = new Vector3(Character_Icon_width * 0.5f + (Screen.width * 0.18f)*2, Character_Icon_height * -0.5f, 0f);

        //stage name text init..
        stageNameText = transform.Find("StageName").gameObject;
        RectTransform stageName_trans = stageNameText.GetComponent<RectTransform>();
        float stageName_text_height = Screen.height*0.5f - (Screen.height * 0.1f);
        stageName_trans.anchoredPosition3D = new Vector2(0f, stageName_text_height);
        stageNameText.GetComponent<Text>().fontSize =(int) Mathf.Round(Screen.height * 0.04f);
        //stageClearPercentText = transform.Find("StageClearPercent").gameObject; 

        stageClearPercentText = transform.Find("StageClearPercent").gameObject;
        RectTransform stageClearPercent_trans = stageClearPercentText.GetComponent<RectTransform>();
        float stageClearPercent_text_height = (-Screen.height * 0.5f) + (Screen.height * 0.1f);
        stageClearPercent_trans.anchoredPosition3D = new Vector2(0f, stageClearPercent_text_height);
        stageClearPercentText.GetComponent<Text>().fontSize = (int)Mathf.Round(Screen.height * 0.04f);

        rightArrow = transform.Find("RightArrow").gameObject;
        leftArrow = transform.Find("LeftArrow").gameObject;

        RectTransform r_arrow_transfrom = rightArrow.GetComponent<RectTransform>();
        RectTransform l_arrow_transfrom = leftArrow.GetComponent<RectTransform>();

        r_arrow_transfrom.sizeDelta = new Vector2(Screen.width * 0.1f, Screen.width * 0.1f);
        l_arrow_transfrom.sizeDelta = new Vector2(Screen.width * 0.1f, Screen.width * 0.1f);
    }

}
