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
        DestroySingletonSound();
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
            try
            {
                script = (BoxControler)GameObject.Find("bronze_box").GetComponent(typeof(BoxControler));
                boxType = 0;
            }
            catch(NullReferenceException e)
            {
                try
                {
                    script = (BoxControler)GameObject.Find("silver_box").GetComponent(typeof(BoxControler));
                    boxType = 1;
                }
                catch (NullReferenceException ee)
                {
                    try
                    {
                        script = (BoxControler)GameObject.Find("gold_box").GetComponent(typeof(BoxControler));
                        boxType = 2;
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
