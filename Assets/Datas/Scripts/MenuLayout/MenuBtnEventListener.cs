using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Animmal.Animmals1;


public class MenuBtnEventListener : MonoBehaviour
{
    bool effect_sound_enable;
    public AudioClip clip_select;
    bool changeable = false;
    GameObject level_btn;
    MenuControler menu_controler;
    GameDataLoader loader;
    InventoryClass inventory;
    float ClickTimer;
    private void Start()
    { 
        DestroySingletonSound();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
//        StaticInfoManager.life = 1;
        effect_sound_enable = DataLoadAndSave.LoadSoundData("effect_sound");
        StartCoroutine("delay");
        level_btn = GameObject.Find("LevelBtn");
        if(SceneManager.GetActiveScene().name.Equals("GreenRoomScene"))
            menu_controler =(MenuControler)GameObject.Find("AudioManager").GetComponent(typeof(MenuControler));
    }

    // Start is called before the first frame update
    private void Update()
    {
        ClickTimer = -Time.deltaTime;
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        changeable = true;
    }



    public void ToggleOnClick(string value)
    {
        if (ClickTimer < 0)
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
        ClickTimer = 0.3f;



    }

    public void ItemOnclick(string name)
    {
        if (ClickTimer < 0)
        {
            AudioSource source = gameObject.GetComponent<AudioSource>();

            if (!effect_sound_enable)
            {
                source.enabled = false;
            }
            source.PlayOneShot(clip_select, 1f);

            if (name.Equals("Shield"))
            {
                if (DataLoadAndSave.LoadShieldItemIsUsing().Equals(1))
                {
                    DataLoadAndSave.SetShieldItemIsUsing(0);
                    GameObject.Find("ShieldParticle").GetComponent<ParticleSystem>().Stop();
                    Debug.Log("shield Item not using");
                }
                else
                {
                    DataLoadAndSave.SetShieldItemIsUsing(1);
                    GameObject.Find("ShieldParticle").GetComponent<ParticleSystem>().Play();
                    Debug.Log("shield Item using");
                }

                return;
            }
            if (name.Equals("Fever"))
            {
                if (DataLoadAndSave.LoadFeverItemIsUsing().Equals(1))
                {
                    DataLoadAndSave.SetFeverItemUsing(0);
                    GameObject.Find("FeverParticle").GetComponent<ParticleSystem>().Stop();
                }
                else
                {
                    DataLoadAndSave.SetFeverItemUsing(1);
                    GameObject.Find("FeverParticle").GetComponent<ParticleSystem>().Play();
                }



                return;
            }
            if (name.Equals("Reward"))
            {
                if (DataLoadAndSave.LoadRewardItemIsUsing().Equals(1))
                {
                    DataLoadAndSave.SetRewardItemUsing(0);
                    GameObject.Find("RewardParticle").GetComponent<ParticleSystem>().Stop();
                }
                else
                {
                    DataLoadAndSave.SetRewardItemUsing(1);
                    GameObject.Find("RewardParticle").GetComponent<ParticleSystem>().Play();
                }

                return;
            }
        }
        ClickTimer = 0.3f;

    }

    

    //set current_stage
    public void BtnOnclick(string name)
    {
        if (ClickTimer < 0)
        {
            AudioSource source = gameObject.GetComponent<AudioSource>();

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
                DataLoadAndSave.SaveLastPlayStageNum(StaticInfoManager.current_stage);
                
                if (lastStage >= StaticInfoManager.current_stage - 1)
                {

                    switch (StaticInfoManager.level)
                    {
                        case 0:
                            //                        SimpleSceneFader.ChangeSceneWithFade("Stage_" + name + "_1");
                            LoadingSceneManager.LoadScene("Stage_" + name + "_1");
                            break;
                        case 1:
                            LoadingSceneManager.LoadScene("Stage_" + name + "_2");
                            break;
                        case 2:
                            LoadingSceneManager.LoadScene("Stage_" + name + "_3");
                            break;
                    }

                }
                else
                {
                    if (GameObject.Find("BonusStageStartDialog(Clone)") == null)
                    {
                        GameObject canvas = Resources.Load("prefabs/etc/BonusStageStartDialog") as GameObject;
                        Instantiate(canvas);
                    }
                    return;
                }
            }
            catch (FormatException e)
            {
                //nothing.
            }



            if (name.Equals("setting"))
            {
                SceneManager.LoadScene("SettingScene");
                return;
            }
            if (name.Equals("shop"))
            {
                //            SSTools.ShowMessage("돈 많이 모아두셨죠?", SSTools.Position.bottom, SSTools.Time.twoSecond);
                SceneManager.LoadScene("ShopScene");
                return;
            }
            if (name.Equals("inventory"))
            {
                SceneManager.LoadScene("InventoryScene");
                return;
            }

            if (name.Equals("level"))
            {
                if (StaticInfoManager.level == 0)
                {
                    SSTools.ShowMessage("난이도 설정 변경 : 보통", SSTools.Position.bottom, SSTools.Time.oneSecond);
                    StaticInfoManager.level = 1;
                    level_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/icon/level_normal");
                    menu_controler.changeCurrentStageInfo();
                }
                else if (StaticInfoManager.level == 1)
                {
                    SSTools.ShowMessage("난이도 설정 변경 : 어려움", SSTools.Position.bottom, SSTools.Time.oneSecond);
                    StaticInfoManager.level = 2;
                    level_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/icon/level_hard");
                    menu_controler.changeCurrentStageInfo();
                }
                else if (StaticInfoManager.level == 2)
                {
                    SSTools.ShowMessage("난이도 설정 변경 : 쉬움", SSTools.Position.bottom, SSTools.Time.oneSecond);
                    StaticInfoManager.level = 0;
                    level_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/icon/level_easy");
                    menu_controler.changeCurrentStageInfo();
                }

                StagePannelSizeInit[] sizeInitor = GameObject.Find("Content").GetComponentsInChildren<StagePannelSizeInit>();
                foreach (StagePannelSizeInit initor in sizeInitor)
                {
                    initor.SetBackgroundAlpha();
                }

                return;
            }

            if (name.Equals("LeftArrow"))
            {
                Debug.Log("left btn clicked");
                StartCoroutine("SwitchLeft");
            }
            if (name.Equals("RightArrow"))
            {
                StartCoroutine("SwitchRight");
            }

            if (name.Equals("select"))
            {
                int selectedId = ((PreviewManager)GameObject.Find("PreviewManager").GetComponent(typeof(PreviewManager))).CurrentItemID;
                loader = new GameDataLoader();
                inventory = loader.LoadData();

                if (inventory.isOwn[selectedId])
                {
                    DataLoadAndSave.SaveSelectedCharator(selectedId);
                    SceneManager.LoadScene("GreenRoomScene");
                }
                else
                {
                    SSTools.ShowMessage("캐릭터를 보유하고 있지 않습니다.", SSTools.Position.bottom, SSTools.Time.twoSecond);
                }



            }

            if (name.Equals("backBtn"))
            {
                SceneManager.LoadScene("GreenRoomScene");
            }

            if (name.Equals("BronzeBox"))
            {
                StaticInfoManager.boxType = 0;
                if (DataLoadAndSave.LoadCoin() >= 1000)
                {
                    DataLoadAndSave.CoinsUsed(1000);
                    SceneManager.LoadScene("BoxScene");
                }
                else
                {
                    SSTools.ShowMessage(StaticInfoManager.lang.getString("CoinRequire"), SSTools.Position.bottom, SSTools.Time.twoSecond);
                }
            }
            if (name.Equals("SilverBox"))
            {
                if (DataLoadAndSave.LoadCoin() >= 4500)
                {
                    StaticInfoManager.boxType = 1;
                    DataLoadAndSave.CoinsUsed(4500);
                    SceneManager.LoadScene("BoxScene");
                }
                else
                {
                    SSTools.ShowMessage(StaticInfoManager.lang.getString("CoinRequire"), SSTools.Position.bottom, SSTools.Time.twoSecond);
                }

            }
            if (name.Equals("GoldBox"))
            {
                if (DataLoadAndSave.LoadCoin() >= 8500)
                {
                    StaticInfoManager.boxType = 2;
                    DataLoadAndSave.CoinsUsed(8500);
                    SceneManager.LoadScene("BoxScene");
                }
                else
                {
                    SSTools.ShowMessage(StaticInfoManager.lang.getString("CoinRequire"), SSTools.Position.bottom, SSTools.Time.twoSecond);
                }
            }
        }

        ClickTimer = 0.3f;

        //Debug.Log(name+"clicked!");
    }

    void DestroySingletonSound()
    {
        Destroy(GameObject.Find("StoryBgm"));
    }

    IEnumerator SwitchLeft()
    {
        menu_controler.lefting = true;
        yield return new WaitForSeconds(0.1f);
        menu_controler.lefting = false;
    }
    IEnumerator SwitchRight()
    {
        menu_controler.righting = true;
        yield return new WaitForSeconds(0.1f);
        menu_controler.righting = false;
    }
}
