using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRewardValue : MonoBehaviour
{
    int fontSize;
    Text ValueCombo, ValuePercent,ValueReward;
    Text MaxText, ClearText, TotalText;
    GameObject ValueRewardObj;
    GameObject stageFailMessage;
    VisibleTimer VisibleTimerOfRewardObj;
    AudioSource ValueRewardAudio;
    public AudioClip coinSound;
    int RealRewardCoins;
    int ViewRewardCoins;

    bool onetimeForGetCoin = true;
    bool onetimeForCoin = true;

    public bool rewardPrcessStatus = false;
    // Start is called before the first frame update
    void Start()
    {
        stageFailMessage =  GameObject.Find("StageFailMessage");

        int lastPercent = DataLoadAndSave.LoadStageClearPercent(StaticInfoManager.current_stage);
        if (lastPercent < StaticInfoManager.clearPercent)
        {
            StaticInfoManager.isNewRecord = true;
            DataLoadAndSave.SaveStageClearPercent(StaticInfoManager.current_stage,(int)StaticInfoManager.clearPercent);
        }


        fontSize = (int)Mathf.Round(Screen.width * 0.06f);
        ViewRewardCoins = 0;
        ValueCombo = GameObject.Find("ValueCombo").GetComponent<Text>();
        ValuePercent = GameObject.Find("ValuePercent").GetComponent<Text>();
        ValueRewardObj = GameObject.Find("ValueReward");
        ValueRewardAudio = ValueRewardObj.GetComponent<AudioSource>();
        ValueReward = ValueRewardObj.GetComponent<Text>();
        VisibleTimerOfRewardObj = (VisibleTimer)ValueRewardObj.GetComponent(typeof(VisibleTimer));
        MaxText = GameObject.Find("MaxCombo").GetComponent<Text>();
        ClearText = GameObject.Find("ClearPercent").GetComponent<Text>();
        TotalText = GameObject.Find("TotalReward").GetComponent<Text>();

        if (StaticInfoManager.clearPercent < 10)
        {
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = "ㅋㅋ..";
        }else if(StaticInfoManager.clearPercent >= 10 && StaticInfoManager.clearPercent < 30)
        {
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = "좀 더 분발해보세요!";
        }
        else if (StaticInfoManager.clearPercent >= 30 && StaticInfoManager.clearPercent < 60)
        {
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = "와 절반쯤 가셨어요!";
        }
        else if (StaticInfoManager.clearPercent >= 60 && StaticInfoManager.clearPercent < 80)
        {
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = "적응해 가시는군";
        }
        else if (StaticInfoManager.clearPercent >= 80 && StaticInfoManager.clearPercent < 100)
        {
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = "아이고 아까워라!";
        }

        if (!StaticInfoManager.isNewRecord)
        {
            Destroy(GameObject.Find("NewRecordMessage"));
        }

        if (!StaticInfoManager.isCleared)
        {
            Destroy(GameObject.Find("StageClearMessage"));
        }
        else
        {
            Destroy(stageFailMessage);
        }

        ValueCombo.text = StaticInfoManager.maxCombo + ValueCombo.text;
        ValuePercent.text = StaticInfoManager.clearPercent + ValuePercent.text;

        RealRewardCoins = CalculateReward();
        DataLoadAndSave.CoinsRewarded(RealRewardCoins);
//        StaticInfoManager.accumulateCoin = RealRewardCoins;

        FontSizeSetting();

        if (!DataLoadAndSave.LoadSoundData("effect_sound"))
        {
            ValueRewardAudio.enabled = false;
        }

        StaticInfoManager.ValueInit();
    }

    void FontSizeSetting()
    {
        ValueReward.fontSize = fontSize;
        ValueCombo.fontSize = fontSize;
        ValuePercent.fontSize = fontSize;
        MaxText.fontSize = fontSize;
        ClearText.fontSize = fontSize;
        TotalText.fontSize = fontSize;

    }
    void PlayGetCoinSoundOneTime()
    {
        if (onetimeForGetCoin)
        {
            onetimeForGetCoin = false;
            ValueRewardAudio.Play();
        }
    }

    void PlayCoinSoundOneTime()
    {
        if (onetimeForCoin)
        {
            onetimeForCoin = false;
            ValueRewardAudio.loop = false;
            if (ValueRewardAudio.isPlaying)
            {
                ValueRewardAudio.Stop();
            }
            ValueRewardAudio.PlayOneShot(coinSound);
        }

    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(VisibleTimerOfRewardObj.times);
        if (VisibleTimerOfRewardObj.times < 0f && ViewRewardCoins<RealRewardCoins)
        {
            PlayGetCoinSoundOneTime();
            if (ViewRewardCoins < RealRewardCoins)
            {
                ViewRewardCoins++;

                if (ViewRewardCoins < RealRewardCoins)
                {
                    ViewRewardCoins++;

                    if (ViewRewardCoins < RealRewardCoins)
                    {
                        ViewRewardCoins++;
                    }
                }
            }
            ValueReward.text = ViewRewardCoins + " Coins";
        }

        if (VisibleTimerOfRewardObj.times < 0f && ViewRewardCoins.Equals(RealRewardCoins))
        {
            PlayCoinSoundOneTime();
            rewardPrcessStatus = true;
        }
    }

    public void ForceCoinRewardProcess()
    {
        ForceViewAllReward();
        VisibleTimerOfRewardObj.times = -1f;
        ViewRewardCoins = RealRewardCoins;
        rewardPrcessStatus = true;
        ValueReward.text = ViewRewardCoins + " Coins";
    }

    public void ForceViewAllReward()
    {
        ((VisibleTimer)GameObject.Find("MaxCombo").GetComponent(typeof(VisibleTimer))).times = 0f;
        ((VisibleTimer)GameObject.Find("ClearPercent").GetComponent(typeof(VisibleTimer))).times = 0f;
        ((VisibleTimer)GameObject.Find("TotalReward").GetComponent(typeof(VisibleTimer))).times = 0f;
        ((VisibleTimer)GameObject.Find("ValueCombo").GetComponent(typeof(VisibleTimer))).times = 0f;
        ((VisibleTimer)GameObject.Find("ValuePercent").GetComponent(typeof(VisibleTimer))).times = 0f;

        try
        {
            if (stageFailMessage != null)
                stageFailMessage.GetComponent<VisibleTimer>().times = 0f;
        }
        catch (MissingReferenceException e)
        {

        }

        try
        {
            GameObject message = GameObject.Find("NewRecordMessage");
            if(message != null)
                message.GetComponent<VisibleTimer>().times = 0f;
        }
        catch (MissingReferenceException e)
        {

        }

        try
        {
            GameObject message = GameObject.Find("StageClearMessage");
            if (message != null)
                message.GetComponent<VisibleTimer>().times = 0f;
        }
        catch (MissingReferenceException e)
        {

        }


    }

    int CalculateReward()
    {
        float value = StaticInfoManager.maxCombo * StaticInfoManager.clearPercent*0.01f;
        if (StaticInfoManager.isCleared)
        {
            value = value * 2f;
        }

        if (StaticInfoManager.isNewRecord)
        {
            value = value * 1.5f;
        }
        if(StaticInfoManager.clearPercent >= 10)
        {
            value = value + StaticInfoManager.current_stage * 50;
        }
        return (int) Mathf.Round(value);
    }
}