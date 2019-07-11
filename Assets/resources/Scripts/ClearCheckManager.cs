using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCheckManager : MonoBehaviour
{
    public GameObject player;
    public GameObject actor_camera;
    AudioSource player_audio;
    PlayerActionControler control;
    PlayerControler player_controler;
    SimpleCameraControl cameraControl;
    public float clearZ;
    public AudioClip clip;

    bool onetime = true;
    // Start is called before the first frame update
    void Start()
    {
        control = (PlayerActionControler)player.GetComponent(typeof(PlayerActionControler));
        cameraControl = (SimpleCameraControl)actor_camera.GetComponent(typeof(SimpleCameraControl));
        player_controler = (PlayerControler) player.GetComponent(typeof(PlayerControler));
        player_audio = player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.z >= clearZ)
        {
            control.isClear = true;
            //            cameraControl.actor = null;
            cameraControl.stop = true;
            //control.StopControl();
//            cameraControl.CameraMoveToY(15f);
            if (onetime)
            {
                onetime = false;
                StartCoroutine("ChangeScene");
            }
        }
    }

    IEnumerator ChangeScene()
    {
        player_audio.enabled = false;

        AudioSource source = gameObject.GetComponent<AudioSource>();
        source.PlayOneShot(clip, 1.0f);
        StaticInfoManager.maxCombo = player_controler.maxCombo;
        StaticInfoManager.clearPercent = 100f;
        StaticInfoManager.isCleared = true;
        DataLoadAndSave.SaveTopClearStage(StaticInfoManager.current_stage);
        //check last clearPercent of stage...

        yield return new WaitForSeconds(3f);
        //change scene
        SimpleSceneFader.ChangeSceneWithFade("RewardScene");
    }
}
