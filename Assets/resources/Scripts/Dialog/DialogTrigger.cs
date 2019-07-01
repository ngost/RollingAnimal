using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    GameObject adMob;
    AdMobManager adMobManager;
    Scene scene;

    public void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        adMob = GameObject.Find("AdMobManager");
        adMobManager = (AdMobManager)adMob.GetComponent(typeof(AdMobManager));
    }
    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);


    }
    public void Response()
    {
        //ad here!

        //MapData.life = 1;
        if (SceneManager.GetActiveScene().name.Equals("GreenRoomScene"))
        {
            adMobManager.ShowRewardAd();
        }
        else
        {
            adMobManager.ShowInterstitialAd();
        }

//        SceneManager.LoadScene(scene.name);
    }

    public void GiveUp()
    {
        SimpleSceneFader.ChangeSceneWithFade("RewardScene");
    }
}
