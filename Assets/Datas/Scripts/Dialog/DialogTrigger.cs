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
            //LoadingSceneManager.LoadScene("Stage_" + StaticInfoManager.current_stage + "_" + (StaticInfoManager.level + 1));
            //Debug.Log("c_stage: "+StaticInfoManager.current_stage);
            //Debug.Log(StaticInfoManager.level);
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

    public void Review()
    {
        DataLoadAndSave.SetReviewingStatus();
        Application.OpenURL("market://details?id=" + Application.identifier);
        Destroy(GameObject.Find("ReviewDialog(Clone)"),2f);
    }

    public void Cancle()
    {
        DataLoadAndSave.SetReviewingStatus();
        Destroy(GameObject.Find("ReviewDialog(Clone)"));
    }
}
