using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GetRewardValue : MonoBehaviour
{
    int fontSize;
    Text ValueCombo, ValuePercent,ValueReward;
    Text MaxText, ClearText, TotalText;
    GameObject ValueRewardObj;
    GameObject stageFailMessage;
    GameObject stageClearMessage, newRecordMessage;
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
        Resources.UnloadUnusedAssets();
        GC.Collect();
        stageFailMessage =  GameObject.Find("StageFailMessage");
        newRecordMessage = GameObject.Find("NewRecordMessage");
        stageClearMessage = GameObject.Find("StageClearMessage");

        int lastPercent = DataLoadAndSave.LoadStageClearPercent(StaticInfoManager.current_stage,StaticInfoManager.level);
        if (lastPercent < StaticInfoManager.clearPercent)
        {
            StaticInfoManager.isNewRecord = true;
            DataLoadAndSave.SaveStageClearPercent(StaticInfoManager.current_stage,(int)StaticInfoManager.clearPercent,StaticInfoManager.level);
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
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = StaticInfoManager.lang.getString("RewardMessage1");
        }else if(StaticInfoManager.clearPercent >= 10 && StaticInfoManager.clearPercent < 30)
        {
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = StaticInfoManager.lang.getString("RewardMessage2");
        }
        else if (StaticInfoManager.clearPercent >= 30 && StaticInfoManager.clearPercent < 60)
        {
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = StaticInfoManager.lang.getString("RewardMessage3");
        }
        else if (StaticInfoManager.clearPercent >= 60 && StaticInfoManager.clearPercent < 80)
        {
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = StaticInfoManager.lang.getString("RewardMessage4");
        }
        else if (StaticInfoManager.clearPercent >= 80 && StaticInfoManager.clearPercent < 100)
        {
            stageFailMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = StaticInfoManager.lang.getString("RewardMessage5");
        }

        if (!StaticInfoManager.isNewRecord)
        {
            Destroy(newRecordMessage);
        }
        else
        {
            newRecordMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = StaticInfoManager.lang.getString("NewRecordMessage");
        }

        if (!StaticInfoManager.isCleared)
        {
            Destroy(stageClearMessage);
        }
        else
        {
            stageClearMessage.GetComponent<RetroPrinterScriptBasic>().CursorCharacter = StaticInfoManager.lang.getString("StageClearMessage");
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
        float value = StaticInfoManager.maxCombo * StaticInfoManager.clearPercent*0.02f;
        if (StaticInfoManager.isCleared)
        {
            value = value * 2f;
        }

        if (StaticInfoManager.isNewRecord)
        {
            value = value * 1.5f;
        }
        if(StaticInfoManager.clearPercent >= 10 && StaticInfoManager.clearPercent<30)
        {
            value = value + StaticInfoManager.current_stage * 50;
        }else if(StaticInfoManager.clearPercent >= 30 && StaticInfoManager.clearPercent < 50)
        {
            value = value + StaticInfoManager.current_stage * 70;
        }
        else if (StaticInfoManager.clearPercent >= 50 && StaticInfoManager.clearPercent < 70)
        {
            value = value + StaticInfoManager.current_stage * 90;
        }
        else if (StaticInfoManager.clearPercent >= 70 && StaticInfoManager.clearPercent <= 100)
        {
            value = value + StaticInfoManager.current_stage * 110;
        }

        if(StaticInfoManager.level == 1)
        {
            value = value * 1.5f;
        }
        else if (StaticInfoManager.level == 2)
        {
            value = value * 2f;
        }

        if (StaticInfoManager.rewardEnable)
        {
            value = value * 1.5f;
        }

        return (int) Mathf.Round(value);
    }
}