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

    public static void SaveTopClearStage(int stageNum)
    {
        if (LoadTopClearStage() < stageNum)
        {
            PlayerPrefs.SetInt("topStage", stageNum);
        }
    }

    public static int LoadTopClearStage()
    {
        return PlayerPrefs.GetInt("topStage", 0);
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
    public static void SaveStageClearPercent(int stageNum,int percent)
    {
        PlayerPrefs.SetInt("stage" + stageNum + "CP",percent);
    }

    public static void AddNewStageName(int stageNum, string name)
    {
        PlayerPrefs.SetString("stage" + stageNum + "Name",name);
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

    public static string LoadStageName(int stageNum)
    {
        string stageName = PlayerPrefs.GetString("stage" + stageNum + "Name", "준비중");
        return stageName;
    }

    public static int LoadStageClearPercent(int stageNum)
    {
        int clearPercent = PlayerPrefs.GetInt("stage" + stageNum + "CP", 0);
        return clearPercent;
    }
}