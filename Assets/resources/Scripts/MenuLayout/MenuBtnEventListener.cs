using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MenuBtnEventListener : MonoBehaviour
{
    bool effect_sound_enable;
    public AudioClip clip_select;
    bool changeable = false;
    GameObject level_btn;
    MenuControler menu_controler;
    private void Start()
    {
        DestroySingletonSound();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StaticInfoManager.life = 1;
        effect_sound_enable = DataLoadAndSave.LoadSoundData("effect_sound");
        StartCoroutine("delay");
        level_btn = GameObject.Find("LevelBtn");
        menu_controler =(MenuControler)GameObject.Find("AudioManager").GetComponent(typeof(MenuControler));
    }

    // Start is called before the first frame update


    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        changeable = true;
    }



    public void ToggleOnClick(string value)
    {
        if (changeable)
        {
            if (value.Equals("back_sound"))
            {
                Debug.Log("back_sound_clicked");
                bool check_status = GameObject.Find("back_sound").GetComponent<Toggle>().isOn;
                StaticInfoManager.background_sound_enable = check_status;
                DataLoadAndSave.SaveSoundData("back_sound", StaticInfoManager.background_sound_enable);

            }
            else if (value.Equals("effect_sound"))
            {
                Debug.Log("effect_sound_clicked");
                bool check_status = GameObject.Find("effect_sound").GetComponent<Toggle>().isOn;
                StaticInfoManager.effect_sound_enable = check_status;
                DataLoadAndSave.SaveSoundData("effect_sound", StaticInfoManager.effect_sound_enable);
            }
        }


    }

    //set current_stage
    public void BtnOnclick(string name)
    {

        AudioSource source = (AudioSource)GameObject.Find("AudioManager").GetComponent<AudioSource>();

        if (!effect_sound_enable)
        {
            source.enabled = false;
        }
        source.PlayOneShot(clip_select, 1f);

        try
        {
            if (name.Equals("-1"))
            {
                SSTools.ShowMessage("열심히 작업중입니다..!", SSTools.Position.bottom, SSTools.Time.twoSecond);
                return;
            }
            int lastStage = DataLoadAndSave.LoadTopClearStage(StaticInfoManager.level);
            StaticInfoManager.current_stage = int.Parse(name);

            if (lastStage >= StaticInfoManager.current_stage - 1)
            {

                switch (StaticInfoManager.level)
                {
                    case 0:
                        SimpleSceneFader.ChangeSceneWithFade("Stage_" + name + "_1");
                        break;
                    case 1:
                        SimpleSceneFader.ChangeSceneWithFade("Stage_" + name + "_2");
                        break;
                    case 2:
                        SimpleSceneFader.ChangeSceneWithFade("Stage_" + name + "_3");
                        break;
                }

            }
            else
            {
                if(GameObject.Find("BonusStageStartDialog(Clone)") == null){
                    GameObject canvas = Resources.Load("prefabs/etc/BonusStageStartDialog") as GameObject;
                    Instantiate(canvas);
                }
                return;
            }
        }
        catch(FormatException e)
        {
            //nothing.
        }



        if (name.Equals("setting"))
        {
            SimpleSceneFader.ChangeSceneWithFade("SettingScene");
            return;
        }
        if (name.Equals("shop"))
        {
            SSTools.ShowMessage("돈 많이 모아두셨죠?", SSTools.Position.bottom, SSTools.Time.twoSecond);
            return;
        }
        if (name.Equals("inventory"))
        {
            SSTools.ShowMessage("빠른 시일 내에 귀여운 캐릭터들을 만나보실 수 있습니다.", SSTools.Position.bottom, SSTools.Time.twoSecond);
            return;
        }
        if (name.Equals("level"))
        {
            if (StaticInfoManager.level == 0)
            {
                SSTools.ShowMessage("난이도 설정 변경 : 보통", SSTools.Position.bottom, SSTools.Time.twoSecond);
                StaticInfoManager.level = 1;
                level_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/level_normal");
                menu_controler.changeCurrentStageInfo();
            }
            else if(StaticInfoManager.level == 1)
            {
                SSTools.ShowMessage("난이도 설정 변경 : 어려움", SSTools.Position.bottom, SSTools.Time.twoSecond);
                StaticInfoManager.level = 2;
                level_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/level_hard");
                menu_controler.changeCurrentStageInfo();
            }
            else if (StaticInfoManager.level == 2)
            {
                SSTools.ShowMessage("난이도 설정 변경 : 쉬움", SSTools.Position.bottom, SSTools.Time.twoSecond);
                StaticInfoManager.level = 0;
                level_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/level_easy");
                menu_controler.changeCurrentStageInfo();
            }

            StagePannelSizeInit[] sizeInitor = GameObject.Find("Content").GetComponentsInChildren<StagePannelSizeInit>();
            foreach (StagePannelSizeInit initor in sizeInitor)
            {
                initor.SetBackgroundAlpha();
            }

            return;
        }
        //Debug.Log(name+"clicked!");
    }

    void DestroySingletonSound()
    {
        Destroy(GameObject.Find("StoryBgm"));
    }

}
