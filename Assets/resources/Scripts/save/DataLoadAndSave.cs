using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataLoadAndSave
{
    public static void SaveTutorialState()
    {
        PlayerPrefs.SetInt("tutorial", 1);
    }
    public static int LoadTutorialState()
    {
        return PlayerPrefs.GetInt("tutorial",0);
    }

    public static void SaveGraphicQuality(int index)
    {
        PlayerPrefs.SetInt("quality", index);
    }

    public static int LoadGraphicQuality()
    {
        int saved_index = PlayerPrefs.GetInt("quality", 2);
        return saved_index;
    }

    public static void SaveTopClearStage(int stageNum,int level = 0)
    {
        switch (level)
        {
            case 0:
                if (LoadTopClearStage(0) < stageNum)
                {
                    PlayerPrefs.SetInt("topStage_easy", stageNum);
                }
                break;
            case 1:
                if (LoadTopClearStage(1) < stageNum)
                {
                    PlayerPrefs.SetInt("topStage_normal", stageNum);
                }
                break;
            case 2:
                if (LoadTopClearStage(2) < stageNum)
                {
                    PlayerPrefs.SetInt("topStage_difficult", stageNum);
                }
                break;
            default:
                break;
        }
    }

    public static int LoadTopClearStage(int level = 0)
    {
        int returnInt = 0;
        switch (level)
        {
            case 0:
                returnInt = PlayerPrefs.GetInt("topStage_easy", 0);
                break;
            case 1:
                returnInt = PlayerPrefs.GetInt("topStage_normal", 0);
                break;
            default:
                returnInt = PlayerPrefs.GetInt("topStage_difficult", 0);
                break;
        }
        return returnInt;

    }
    public static void SaveSoundData(string soundType,bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt(soundType, 1);
        }
        else
        {
            PlayerPrefs.SetInt(soundType, 0);
        }
    }
    public static bool LoadSoundData(string soundType)
    {
        int value = PlayerPrefs.GetInt(soundType, 1);
        if (value == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //private int coins;
    public static void SaveStageClearPercent(int stageNum,int percent, int level = 0)
    {
        PlayerPrefs.SetInt("stage" + stageNum + "CP"+level,percent);
    }
    public static int LoadStageClearPercent(int stageNum,int level = 0)
    {
        int clearPercent = PlayerPrefs.GetInt("stage" + stageNum + "CP"+level, 0);
        return clearPercent;
    }

   

    private static void SaveCoins(int coins)
    {
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.Save();
        Debug.Log("saved coins :" + coins);
    }

    public static int LoadCoin()
    {
        int coins = PlayerPrefs.GetInt("coins");
        Debug.Log("coins :"+ coins);
        return coins;
    }

    public static void CoinsRewarded(int reward)
    {
        int currentCoins = LoadCoin();
        SaveCoins(currentCoins + reward);
    }

    public static bool CoinsUsed(int used)
    {
        int coins = LoadCoin();
        if(coins >= used)
        {
            coins = coins - used;
            SaveCoins(coins);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void LoadOwnCharactorsList()
    {
        string _json = PlayerPrefs.GetString("charactors");
        //List<string> _string_list = _json.
    }

    //very important
    public static void AddNewStageName(int stageNum, string name)
    {
        PlayerPrefs.SetString("stage" + stageNum + "Name", name);
    }

    public static string LoadStageName(int stageNum)
    {
        string stageName = PlayerPrefs.GetString("stage" + stageNum + "Name", StaticInfoManager.lang.getString("stage_coming"));
        return stageName;
    }


}