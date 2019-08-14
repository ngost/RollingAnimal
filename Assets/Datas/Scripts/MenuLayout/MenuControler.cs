

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MenuControler : MonoBehaviour
{
    GameObject panel;
    Rigidbody panel_rg;
    RectTransform panel_transform;
    float alphaX;
    int last_stage_number;
    AudioSource m_audioSource;
    public AudioClip[] stage_clip;
    public int total_stage_num;
    GameObject scroll;
    MyScrollRect scrollRect;
    Image leftArrow, rightArrow;
    Text stageNameText, stageClearPercentText;
    public bool isTouching;
    GameObject level_btn;
    public bool lefting = false, righting = false;
    GameObject Content;
    // Start is called before the first frame update
    void Start()
    {
        //level btn load and init

        level_btn = GameObject.Find("LevelBtn");
        if (StaticInfoManager.level == 0)
        {
            level_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/icon/level_easy");
        }
        else if (StaticInfoManager.level == 1)
        {
            level_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/icon/level_normal");
        }
        else if (StaticInfoManager.level == 2)
        {
            level_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/icon/level_hard");
        }

        //stage pannel size init
        Content = GameObject.Find("Content");
        StagePannelSizeInit[] sizeInitor = Content.GetComponentsInChildren<StagePannelSizeInit>();
        foreach (StagePannelSizeInit initor in sizeInitor)
        {
            initor.SetBackgroundAlpha();
        }
        //Stage Name Init
        DataLoadAndSave.AddNewStageName(1, StaticInfoManager.lang.getString("stage1_name"));
        DataLoadAndSave.AddNewStageName(2, StaticInfoManager.lang.getString("stage2_name"));
        DataLoadAndSave.AddNewStageName(3, StaticInfoManager.lang.getString("stage3_name"));
        DataLoadAndSave.AddNewStageName(4, StaticInfoManager.lang.getString("stage4_name"));
        DataLoadAndSave.AddNewStageName(5, StaticInfoManager.lang.getString("stage5_name"));

        //stage_clip = new AudioClip[total_stage_num];
        last_stage_number = 0;
        panel = Content;
        panel_transform = panel.GetComponent<RectTransform>();
        panel_rg = panel.GetComponent<Rigidbody>();
        scroll = panel.transform.parent.gameObject.transform.parent.gameObject;
        scrollRect = (MyScrollRect)scroll.GetComponent(typeof(MyScrollRect));

        alphaX = panel_transform.sizeDelta.x * 0.5f;

        //최근 진행한 스테이지로 첫화면 이동
        panel_transform.localPosition = new Vector3(-Screen.width * (DataLoadAndSave.LoadLastPlayStageNum() - 1), panel_transform.localPosition.y, panel_transform.localPosition.z);

        m_audioSource = gameObject.GetComponent<AudioSource>();
        //stage_clip[0] = Resources.Load("music/menu_stage1") as AudioClip;
        //stage_clip[1] = Resources.Load("music/menu_stage2") as AudioClip;
        //stage_clip[2] = Resources.Load("music/menu_stage3") as AudioClip;
        //stage_clip[3] = Resources.Load("music/menu_stage4") as AudioClip;
        //stage_clip[4] = Resources.Load("music/menu_stage5") as AudioClip;
        stageNameText = GameObject.Find("StageName").GetComponent<Text>();
        stageClearPercentText = GameObject.Find("StageClearPercent").GetComponent<Text>();
        leftArrow = GameObject.Find("LeftArrow").GetComponent<Image>();
        rightArrow = GameObject.Find("RightArrow").GetComponent<Image>();

        //init only stage 1
        string stageName = DataLoadAndSave.LoadStageName(1);
        int clearPercent = DataLoadAndSave.LoadStageClearPercent(1,StaticInfoManager.level);
        stageNameText.text = stageName;
        stageClearPercentText.text = clearPercent + " % " + StaticInfoManager.lang.getString("clear");

        if (!DataLoadAndSave.LoadSoundData("back_souned"))
        {
            m_audioSource.Stop();
        }
    }



    // Update is called once per frame
    void Update()
    {
        try
        {
            //this is current panel position. first is 0.
            float x = panel_transform.localPosition.x;
            // item to item width is 1500.
            // x / 750 = 0.xx,  1.xxx, etc...
            x = Mathf.Abs(x);
            x = x + Screen.width * 0.5f;
            //x = x - 3000;
            //Debug.Log(x);
            int current_stage_number = (int)(x / Screen.width);

            float xMagnatitude = current_stage_number * Screen.width;
            // xMagnatitude = xMagnatitude;
            //Debug.Log(scrollRect.velocity);
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                isTouching = true;
            }
            else
            {
                isTouching = false;
            }


            if (!isTouching)
            {
                if (lefting)
                {
                    Debug.Log("lefting");
                    GotoLeftPannel(current_stage_number);
                    return;
                }
                else if (righting)
                {
                    Debug.Log("righting");
                    GotoRightPannel(current_stage_number);
                    return;
                }

                if (Mathf.Abs(scrollRect.velocity.x) < 600)
                {
                    scrollRect.velocity = new Vector2(0, 0);
                    panel_transform.localPosition = Vector3.Lerp(panel_transform.localPosition, new Vector3(-Screen.width * current_stage_number, panel_transform.localPosition.y, panel_transform.localPosition.z), 10f * Time.deltaTime);
                    stageNameText.enabled = true;
                    stageClearPercentText.enabled = true;
                    leftArrow.enabled = true;
                    rightArrow.enabled = true;
                }
                else
                {
                    //화면이 스크롤중일때
                    stageNameText.enabled = false;
                    stageClearPercentText.enabled = false;
                    leftArrow.enabled = false;
                    rightArrow.enabled = false;
                    //Debug.Log(scrollRect.velocity);
                }
                if (last_stage_number.Equals(current_stage_number))
                {
                    //not thing;
                }
                else
                {
                    //if, viewing stage is change, save last stage number.
                    last_stage_number = current_stage_number;
                    // change sound of current stage sound.
                    m_audioSource.Stop();

                    // new audio play!
                    try
                    {
                        if (DataLoadAndSave.LoadSoundData("back_sound"))
                        {
                            m_audioSource.clip = stage_clip[current_stage_number];
                            m_audioSource.Play();
                            m_audioSource.loop = true;
                        }
                        // get stage name and percent
                        changeCurrentStageInfo();
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Debug.Log("index out");
                    }
                }
            }
        }
        catch(MissingReferenceException e)
        {

        }
    }

    public void changeCurrentStageInfo()
    {
        string stageName = DataLoadAndSave.LoadStageName(last_stage_number + 1);
        int clearPercent = DataLoadAndSave.LoadStageClearPercent(last_stage_number + 1, StaticInfoManager.level);
        stageNameText.text = stageName;
        stageClearPercentText.text = clearPercent + " % "+StaticInfoManager.lang.getString("clear");
    }

    public void GotoLeftPannel(int current_stage_number)
    {
        panel_transform.localPosition = Vector3.Lerp(panel_transform.localPosition, new Vector3(panel_transform.localPosition.x + Screen.width*0.75f, panel_transform.localPosition.y, panel_transform.localPosition.z), 10f * Time.deltaTime);
    }

    public void GotoRightPannel(int current_stage_number)
    {
        panel_transform.localPosition = Vector3.Lerp(panel_transform.localPosition, new Vector3(panel_transform.localPosition.x - Screen.width * 0.75f, panel_transform.localPosition.y, panel_transform.localPosition.z), 10f * Time.deltaTime);
    }

}
