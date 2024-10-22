﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading.Tasks;

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
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXEditor)
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
                        if (Application.platform == RuntimePlatform.Android)
                        {
                            new AndroidJavaClass("java.lang.System").CallStatic("exit", 0);
                        }
                        Application.Quit();
                        return;
                    }


                }

                SceneManager.LoadScene(Scene_Name);
//             SimpleSceneFader.ChangeSceneWithFade(Scene_Name);
                

            }
        }
    }
}
