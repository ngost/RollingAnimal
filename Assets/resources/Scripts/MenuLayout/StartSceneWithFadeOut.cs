using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneWithFadeOut : MonoBehaviour
{
    AudioSource source;
    public AudioClip clip;
    public string sceneName = "";
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
        }
        Debug.Log("clicked");
        source.PlayOneShot(clip,1.0f);
        //StaticInfoManager.current_stage = 0;
        SimpleSceneFader.ChangeSceneWithFade(sceneName);
    }

    void DestroySingletonSound()
    {
        Destroy(GameObject.Find("StoryBgm"));
    }
}
