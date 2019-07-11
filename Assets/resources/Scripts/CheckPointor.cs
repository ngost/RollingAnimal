using UnityEngine;
using System.Collections;

public class CheckPointor : MonoBehaviour
{
    bool onetime = true;
    ParticleSystem particle;
    AudioSource audio;
    // Use this for initialization
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (onetime)
        {
            Debug.Log("checkPoint");
            onetime = false;
            StaticInfoManager.last_checkpoint = new Vector3(transform.position.x,0f, transform.position.z);

            //ParticleSystem 
            particle = gameObject.GetComponentInChildren<ParticleSystem>();
            ParticleSystem.MainModule settings = particle.main;
            settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.514f, 0.568f, 1f), new Color(0.584f, 0.542f, 1f));
            audio.Play();
        }
    }
}
