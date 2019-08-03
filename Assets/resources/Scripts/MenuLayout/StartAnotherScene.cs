using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StartAnotherScene : MonoBehaviour
{
    AudioSource source;
    bool onetime = true;
    // Start is called before the first frame update

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        if (!DataLoadAndSave.LoadSoundData("effect_sound"))
        {
            source.enabled = false;
        }
//        DestroySingletonSound();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
//        StaticInfoManager.life = 1;
    }

    // Update is called once per frame
    void Update()
    {


    }
    void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name.Equals("RewardScene"))
        {
            GetRewardValue script = (GetRewardValue)GameObject.Find("ValueGettor").GetComponent(typeof(GetRewardValue));
            if (!script.rewardPrcessStatus)
            {
                script.ForceCoinRewardProcess();
                return;
            }
            else
            {
                SimpleSceneFader.ChangeSceneWithFade("GreenRoomScene");
            }
        }

        if (SceneManager.GetActiveScene().name.Equals("BoxScene"))
        {
            int boxType = 0;
            BoxControler script = null;
            GameObject[] item = new GameObject[5];
            ItemManager itemManager = new ItemManager(StaticInfoManager.boxType);
            try
            {
                script = (BoxControler)GameObject.Find("bronze_box").GetComponent(typeof(BoxControler));
                boxType = 0;
                //                item[0] = (GameObject)Resources.Load("prefabs/Item/Shield");
                item[0] = itemManager.GetRandomItem();
                
            }
            catch(NullReferenceException e)
            {
                try
                {
                    script = (BoxControler)GameObject.Find("silver_box").GetComponent(typeof(BoxControler));
                    boxType = 1;
                    item[0] = itemManager.GetRandomItem();
                    item[1] = itemManager.GetRandomItem();
                    item[2] = itemManager.GetRandomItem();
                }
                catch (NullReferenceException ee)
                {
                    try
                    {
                        script = (BoxControler)GameObject.Find("gold_box").GetComponent(typeof(BoxControler));
                        boxType = 2;
                        item[0] = itemManager.GetRandomItem();
                        item[1] = itemManager.GetRandomItem();
                        item[2] = itemManager.GetRandomItem();
                        item[3] = itemManager.GetRandomItem();
                        item[4] = itemManager.GetRandomItem();
                    }
                    catch(NullReferenceException eee)
                    {

                    }
                }
            }

            if (script.canTouch && script != null)
            {
                source.Play();
                script.ItemViewing(boxType);
                switch (StaticInfoManager.boxType)
                {
                    case 0:
                        Instantiate(item[0], GameObject.Find("bronze_place_item_1").transform);
                        break;
                    case 1:
                        Instantiate(item[0], GameObject.Find("silver_place_item_1").transform);
                        Instantiate(item[1], GameObject.Find("silver_place_item_2").transform);
                        Instantiate(item[2], GameObject.Find("silver_place_item_3").transform);
                        break;
                    case 2:
                        Instantiate(item[0], GameObject.Find("gold_place_item_1").transform);
                        Instantiate(item[1], GameObject.Find("gold_place_item_2").transform);
                        Instantiate(item[2], GameObject.Find("gold_place_item_3").transform);
                        Instantiate(item[3], GameObject.Find("gold_place_item_4").transform);
                        Instantiate(item[4], GameObject.Find("gold_place_item_5").transform);
                        break;
                }

                return;
            }
            else
            {
                return;
            }
        }

//        Debug.Log("clicked");


        if (SceneManager.GetActiveScene().name.Equals("TitleScene"))
        {
            if (onetime)
            {

                //StaticInfoManager.current_stage = 0;
                source.Play();
                if (DataLoadAndSave.LoadTutorialState().Equals(0))
                {
                    SimpleSceneFader.ChangeSceneWithFade("Tutorial");
                }
                else
                {
                    SimpleSceneFader.ChangeSceneWithFade("GreenRoomScene");
                }
                onetime = false;
            }
            else
            {

            }
        }

    }

    void DestroySingletonSound()
    {
        Destroy(GameObject.Find("StoryBgm"));
    }
}
