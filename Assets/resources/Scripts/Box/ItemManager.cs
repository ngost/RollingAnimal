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
    GameDataLoader loader;
    InventoryClass inventory;
    int index;
    GameObject returnObj = null;

    public ItemManager(int boxType)
	{
        this.BoxType = boxType;

        loader = new GameDataLoader();
        inventory = loader.LoadData();
    }

	public GameObject GetRandomItem()
	{
        int itemType = UnityEngine.Random.Range(1, 11);
        if(itemType>=1 && itemType <= 3)
        {
            //shied
            inventory.shieldItem++;
            loader.WriteData(inventory);
            return (GameObject)Resources.Load("prefabs/Item/Shield");
        }
        else if(itemType >= 4 && itemType <= 6)
        {
            //fever
            inventory.feverItem++;
            loader.WriteData(inventory);
            return (GameObject)Resources.Load("prefabs/Item/FeverDescount");
        }
        else if(itemType >= 7 && itemType <= 9)
        {
            //reward
            inventory.rewardItem++;
            loader.WriteData(inventory);
            return (GameObject)Resources.Load("prefabs/Item/IncreaseReward");
        }
        else
        {
            //charactor
            GameObject tempObj = GetRandomCharactor(BoxType);
            loader.WriteData(inventory);
            return tempObj;
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


        //index 0 is defalt, normal 1~3 , rare 4~12 , epic 13~17 , regand 18~21
        

        //inventory.characterClass;
        //inventory.characterNum;
        //inventory.isOwn;
        //inventory.prefabPath;

        
        //상자에 따른 확률 보정
        switch (boxType)
        {
            //브론즈 상자의 경우 캐릭터 등급 결정
            case 0:
                if (classType >= 1 && classType <= 50 )
                {
                    //보통 등급의 가지고 있지 않은 캐릭터 흭득
                    index = UnityEngine.Random.Range(1, 4);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/1000Coin");
                        DataLoadAndSave.CoinsRewarded(1000);
                    }

                }
                else if (classType >= 51 && classType <= 80)
                {
                    //레어 등급의 가지고 있지 않은 캐릭터 흭득
                    index = UnityEngine.Random.Range(4, 13);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/3000Coin");
                        DataLoadAndSave.CoinsRewarded(3000);
                    }
                }
                else if (classType >= 81 && classType <= 95)
                {
                    //에픽 등급의 가지고 있지 않은 캐릭터 흭득
                    index = UnityEngine.Random.Range(13, 18);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/5000Coin");
                        DataLoadAndSave.CoinsRewarded(5000);
                    }
                }
                else if (classType >= 96 && classType <= 100)
                {
                    //전설 등급의 가지고 있지 않은 캐릭터 흭득
                    index = UnityEngine.Random.Range(18, 22);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/10000Coin");
                        DataLoadAndSave.CoinsRewarded(10000);
                    }
                }
                break;
            case 1:
                //실버 상자의 경우 캐릭터 등급 결정
                if (classType >= 1 && classType <= 46)
                {
                    index = UnityEngine.Random.Range(1, 4);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/1000Coin");
                        DataLoadAndSave.CoinsRewarded(1000);
                    }
                }
                else if (classType >= 47 && classType <= 76)
                {
                    index = UnityEngine.Random.Range(4, 13);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/3000Coin");
                        DataLoadAndSave.CoinsRewarded(3000);
                    }
                }
                else if (classType >= 77 && classType <= 94)
                {
                    index = UnityEngine.Random.Range(13, 18);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/5000Coin");
                        DataLoadAndSave.CoinsRewarded(5000);
                    }
                }
                else if (classType >= 95 && classType <= 100)
                {
                    index = UnityEngine.Random.Range(18, 22);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/10000Coin");
                        DataLoadAndSave.CoinsRewarded(10000);
                    }
                }
                break;
            case 2:
                //골드 상자의 경우 캐릭터 등급 결정
                if (classType >= 1 && classType <= 42)
                {
                    index = UnityEngine.Random.Range(1, 4);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/1000Coin");
                        DataLoadAndSave.CoinsRewarded(1000);
                    }
                }
                else if (classType >= 43 && classType <= 72)
                {
                    index = UnityEngine.Random.Range(4, 13);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/3000Coin");
                        DataLoadAndSave.CoinsRewarded(3000);
                    }
                }
                else if (classType >= 73 && classType <= 93)
                {
                    index = UnityEngine.Random.Range(13, 18);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/5000Coin");
                        DataLoadAndSave.CoinsRewarded(5000);
                    }
                }
                else if (classType >= 94 && classType <= 100)
                {
                    index = UnityEngine.Random.Range(18, 22);
                    if (!inventory.isOwn[index])
                    {
                        inventory.isOwn[index] = true;
                        returnObj = (GameObject)Resources.Load(inventory.prefabPath[index]);
                    }
                    else
                    {
                        returnObj = (GameObject)Resources.Load("prefabs/Item/10000Coin");
                        DataLoadAndSave.CoinsRewarded(10000);
                    }
                }
                break;
            default:
                DataLoadAndSave.CoinsRewarded(1000);
                returnObj = (GameObject)Resources.Load("prefabs/Item/1000Coin");
                break;
        }
        //save code add

        return returnObj;
    }
}
