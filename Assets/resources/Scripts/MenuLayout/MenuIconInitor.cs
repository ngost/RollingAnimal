using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuIconInitor : MonoBehaviour
{
    GameObject coinIcon;
    GameObject settingIcon;
    GameObject shopIcon;
    GameObject characterIcon;
    GameObject stageNameText;
    GameObject stageClearPercentText;
    GameObject rightArrow,leftArrow;
    GameObject levelIcon;
    GameObject backIcon;
    GameObject bronzeBuyIcon;
    GameObject silverBuyIcon;
    GameObject goldBuyIcon;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("GreenRoomScene"))
        {
            //coin icon layout init
            coinIcon = transform.Find("CoinsBtn").gameObject;
            RectTransform coin_trans = coinIcon.GetComponent<RectTransform>();
            float Coin_Icon_width = Screen.width * 0.4f;
            float Coin_Icon_height = Coin_Icon_width * 0.28f;
            coin_trans.sizeDelta = new Vector2(Coin_Icon_width, Coin_Icon_height);
            coin_trans.anchoredPosition3D = new Vector3(Coin_Icon_width * -0.5f, Coin_Icon_height * -0.5f, 0f);
            coinIcon.transform.Find("coinText").GetComponent<Text>().fontSize = Mathf.RoundToInt(Screen.width * 0.05f);

            //level icon layout init
            levelIcon = transform.Find("LevelBtn").gameObject;
            RectTransform level_trans = levelIcon.GetComponent<RectTransform>();
            float Level_Icon_height = Screen.height / 8;
            float Level_Icon_width = Level_Icon_height * 0.67f;

            level_trans.sizeDelta = new Vector2(Level_Icon_width, Level_Icon_height);
            level_trans.anchoredPosition3D = new Vector3((Screen.width * 0.5f) - (Level_Icon_width * 0.5f), -(Screen.height * 0.5f) + (Level_Icon_height * 0.5f));
            //levelIcon.transform.Find("levelText").GetComponent<Text>().fontSize = Mathf.RoundToInt(Screen.width * 0.05f);


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
            character_trans.anchoredPosition3D = new Vector3(Character_Icon_width * 0.5f + (Screen.width * 0.18f) * 2, Character_Icon_height * -0.5f, 0f);

            //stage name text init..
            stageNameText = transform.Find("StageName").gameObject;
            RectTransform stageName_trans = stageNameText.GetComponent<RectTransform>();
            float stageName_text_height = Screen.height * 0.5f - (Screen.height * 0.1f);
            stageName_trans.anchoredPosition3D = new Vector2(0f, stageName_text_height);
            stageNameText.GetComponent<Text>().fontSize = (int)Mathf.Round(Screen.height * 0.04f);
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
        else if (SceneManager.GetActiveScene().name.Equals("ShopScene"))
        {
            int ScreenWidth = Screen.width;
            int ScreenHeight = Screen.height;
            //coin icon layout init
            coinIcon = transform.Find("CoinsBtn").gameObject;
            RectTransform coin_trans = coinIcon.GetComponent<RectTransform>();
            float Coin_Icon_width = ScreenWidth * 0.4f;
            float Coin_Icon_height = Coin_Icon_width * 0.28f;
            coin_trans.sizeDelta = new Vector2(Coin_Icon_width, Coin_Icon_height);
            coin_trans.anchoredPosition3D = new Vector3(Coin_Icon_width * -0.5f, Coin_Icon_height * -0.5f, 0f);
            coinIcon.transform.Find("coinText").GetComponent<Text>().fontSize = Mathf.RoundToInt(ScreenWidth * 0.05f);

            //backbtn icon layout init
            backIcon = transform.Find("BackBtn").gameObject;
            RectTransform back_trans = backIcon.GetComponent<RectTransform>();
            float Back_Icon_width = ScreenWidth * 0.18f;
            float Back_Icon_height = Coin_Icon_height;
            back_trans.sizeDelta = new Vector2(Back_Icon_width, Back_Icon_height);
            back_trans.anchoredPosition3D = new Vector3(Back_Icon_width * 0.5f, Back_Icon_height * -0.5f, 0f);

            //bronze box buy icon layout init
            bronzeBuyIcon = transform.Find("BronzeBuyBtn").gameObject;
            RectTransform bronze_trans = bronzeBuyIcon.GetComponent<RectTransform>();
            float Bronze_Icon_width = ScreenWidth * 0.4f;
            float Bronze_Icon_height = Bronze_Icon_width * 0.28f;
            bronze_trans.sizeDelta = new Vector2(Bronze_Icon_width, Bronze_Icon_height);
            bronze_trans.anchoredPosition3D = new Vector3(Bronze_Icon_width * -0.5f - ScreenWidth * 0.05f, -ScreenHeight * 0.27f, 0f);
            bronzeBuyIcon.transform.Find("BronzeCoinText").GetComponent<Text>().fontSize = Mathf.RoundToInt(ScreenWidth * 0.07f);
            Text BronzeBoxText = bronzeBuyIcon.transform.Find("ObjectName").GetComponent<Text>();
            BronzeBoxText.fontSize = Mathf.RoundToInt(ScreenWidth * 0.07f);
            BronzeBoxText.text = StaticInfoManager.lang.getString("bronze_box_name");

            //bronze box buy icon layout init
            silverBuyIcon = transform.Find("SilverBuyBtn").gameObject;
            RectTransform silver_trans = silverBuyIcon.GetComponent<RectTransform>();
            float Silver_Icon_width = ScreenWidth * 0.4f;
            float Silver_Icon_height = Silver_Icon_width * 0.28f;
            silver_trans.sizeDelta = new Vector2(Silver_Icon_width, Silver_Icon_height);
            silver_trans.anchoredPosition3D = new Vector3(Silver_Icon_width * -0.5f - ScreenWidth * 0.05f, -ScreenHeight * 0.52f, 0f);
            silverBuyIcon.transform.Find("SilverCoinText").GetComponent<Text>().fontSize = Mathf.RoundToInt(ScreenWidth * 0.07f);
            Text SilverBoxText = silverBuyIcon.transform.Find("ObjectName").GetComponent<Text>();
            SilverBoxText.fontSize = Mathf.RoundToInt(ScreenWidth * 0.07f);
            SilverBoxText.text = StaticInfoManager.lang.getString("silver_box_name");

            //bronze box buy icon layout init
            goldBuyIcon = transform.Find("GoldBuyBtn").gameObject;
            RectTransform gold_trans = goldBuyIcon.GetComponent<RectTransform>();
            float Gold_Icon_width = ScreenWidth * 0.4f;
            float Gold_Icon_height = Gold_Icon_width * 0.28f;
            gold_trans.sizeDelta = new Vector2(Gold_Icon_width, Gold_Icon_height);
            gold_trans.anchoredPosition3D = new Vector3(Gold_Icon_width * -0.5f - ScreenWidth * 0.05f, -ScreenHeight * 0.77f, 0f);
            goldBuyIcon.transform.Find("GoldCoinText").GetComponent<Text>().fontSize = Mathf.RoundToInt(ScreenWidth * 0.07f);
            Text GoldBoxText = goldBuyIcon.transform.Find("ObjectName").GetComponent<Text>();
            GoldBoxText.fontSize = Mathf.RoundToInt(ScreenWidth * 0.07f);
            GoldBoxText.text = StaticInfoManager.lang.getString("gold_box_name");

            //inventory Icon layout init
            characterIcon = transform.Find("CharacterBtn").gameObject;
            RectTransform character_trans = characterIcon.GetComponent<RectTransform>();
            float Character_Icon_width = Screen.width * 0.18f;
            float Character_Icon_height = Coin_Icon_height;
            character_trans.sizeDelta = new Vector2(Character_Icon_width, Character_Icon_height);
            character_trans.anchoredPosition3D = new Vector3(Character_Icon_width * 0.5f + (Screen.width * 0.18f) * 1, Character_Icon_height * -0.5f, 0f);
        }

    }

}
