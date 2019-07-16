﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackBtnManager : MonoBehaviour
{
    public string Scene_Name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (Scene_Name.Equals("Quit"))
                {
                    GameObject dialog = GameObject.Find("BonusStageStartDialog(Clone)");
                    if(dialog != null)
                    {
                        Destroy(dialog);
                        return;
                    }
                    else
                    {
                        Application.Quit();
                        return;
                    }


                }

                if (SceneManager.GetActiveScene().name.Equals("StoryMapScene"))
                {
                    SimpleSceneFader.ChangeSceneWithFade("GreenRoomScene");
                } else {
                    SimpleSceneFader.ChangeSceneWithFade(Scene_Name);
                }

            }
        }
    }
}