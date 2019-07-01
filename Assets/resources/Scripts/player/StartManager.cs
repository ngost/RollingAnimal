using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    GameObject actor;
    PlayerActionControler actor_script;
    MeshRenderer actor_render;
    float disovleTime = 30f;
    Shader originShader;
    bool readyToAppear = false;

    // Start is called before the first frame update
    void Start()
    {
        actor =  GameObject.Find("actor");
        //actor_script = (SimpleControl)actor.GetComponent(typeof(SimpleControl));
        //actor_script.gravityScale = 0;
        //actor_script.jumpCoolTime = 9999f;
     //   StartDisovle();

//        DataLoadAndSave.CoinsRewarded(5);
    }

    void OnGUI()
    {
        GUI.color = Color.clear;
        GUI.Button(new Rect(0, 0, 1, 1), "");
    }

    // Update is called once per frame
    void Update()
    {
        //if (readyToAppear)
        //{
        //    if(actor_render != null)
        //    {
        //        actor_render.material.SetFloat("_DisolveTime", disovleTime);
        //        disovleTime--;
        //    }
        //    else
        //    {
        //        Debug.Log("actor_desappear materail null");
        //    }

        //}

        //if (Input.anyKey)
        //{
        //    //actor_script.gravityScale = 1.2f;
        //    //actor_script.jumpCoolTime = 0f;

        //    Debug.Log("stage started");
        //    if (originShader != null)
        //    {
        //        actor_render.material.shader = originShader;
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        Debug.Log("shader null");
        //    }

        //}
    }

    //void StartDisovle()
    //{
    //    actor_render = actor.GetComponent<MeshRenderer>();
    //    Shader disappear = ResourceManager.disappear_shader;
    //    originShader = actor_render.material.shader;

    //    if(disappear != null)
    //    {

    //        actor_render.material.shader = disappear;
    //        AppearSound(actor.GetComponent<AudioSource>());
    //        readyToAppear = true;
    //    }
    //    else
    //    {
    //        Debug.Log("disappear null");
    //    }
    //}

    void DisappearSound(AudioSource audio)
    {
        audio.pitch = 1.5f;
        AudioClip clip = Resources.Load<AudioClip>("music/disappear_bgm");
        audio.PlayOneShot(clip, 1f);
    }
    void AppearSound(AudioSource audio)
    {
        audio.pitch = 1f;
        AudioClip clip = Resources.Load<AudioClip>("music/appear_bgm");
        audio.PlayOneShot(clip, 1f);
    }

}
