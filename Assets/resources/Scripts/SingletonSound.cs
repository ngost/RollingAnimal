using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonSound : MonoBehaviour
{
    private static SingletonSound _instance;

    public static SingletonSound Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SingletonSound>();


                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }


    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;

            DontDestroyOnLoad(this);

            gameObject.GetComponent<AudioSource>().enabled = DataLoadAndSave.LoadSoundData("back_sound");
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
            {
                this.gameObject.GetComponent<AudioSource>().enabled = StaticInfoManager.background_sound_enable;
                Destroy(this.gameObject);
            }

        }
    }
}
