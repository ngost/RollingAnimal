using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ItemManager
{
	GameObject item = null;
    int BoxType;
    int CharactorClass;

	public ItemManager(int boxType)
	{
        this.BoxType = boxType;
	}

	public GameObject GetRandomItem()
	{
        int itemType = UnityEngine.Random.Range(1, 11);
        if(itemType>=1 && itemType <= 3)
        {
            //shied
            return (GameObject)Resources.Load("prefabs/Item/Shield");
        }
        else if(itemType >= 4 && itemType <= 6)
        {
            //fever
            return (GameObject)Resources.Load("prefabs/Item/FeverDescount");
        }
        else if(itemType >= 7 && itemType <= 9)
        {
            //reward
            return (GameObject)Resources.Load("prefabs/Item/IncreaseReward");
        }
        else
        {
            //charactor
            return GetRandomCharactor(BoxType);
        }



	}

    public GameObject GetRandomCharactor(int boxType)
    {
        //캐릭터 등급을 결정지을 파라미터 흭득, 개봉 상자 타입에 따라 확률 조정
        //bronze 1~50, 51~80, 81~95,96~100
        //silver 1~46, 47~76, 77~93,94~100
        //gold 1~42, 43~72, 73~91,92~100

        //랜덤 변수 생성
        int classType = UnityEngine.Random.Range(1, 101);


        //상자에 따른 확률 보정
        switch (boxType)
        {
            //브론즈 상자의 경우 캐릭터 등급 결정
            case 0:
                if (classType >= 1 && classType <= 50 )
                {
                    //보통 등급의 가지고 있지 않은 캐릭터 흭득
                    return null;
                }
                else if (classType >= 51 && classType <= 80)
                {
                    //레어 등급의 가지고 있지 않은 캐릭터 흭득
                    return null;
                }
                else if (classType >= 81 && classType <= 95)
                {
                    //에픽 등급의 가지고 있지 않은 캐릭터 흭득
                    return null;
                }
                else if (classType >= 96 && classType <= 100)
                {
                    //전설 등급의 가지고 있지 않은 캐릭터 흭득
                    return null;
                }
                break;
            case 1:
                //실버 상자의 경우 캐릭터 등급 결정
                if (classType >= 1 && classType <= 46)
                {
                    return null;
                }
                else if (classType >= 47 && classType <= 76)
                {
                    return null;
                }
                else if (classType >= 77 && classType <= 93)
                {
                    return null;
                }
                else if (classType >= 94 && classType <= 100)
                {
                    return null;
                }
                break;
            case 2:
                //골드 상자의 경우 캐릭터 등급 결정
                if (classType >= 1 && classType <= 42)
                {
                    return null;
                }
                else if (classType >= 43 && classType <= 72)
                {
                    return null;
                }
                else if (classType >= 73 && classType <= 91)
                {
                    return null;
                }
                else if (classType >= 92 && classType <= 100)
                {
                    return null;
                }
                break;
            default:
                return null;
        }
        return null;
    }
}
