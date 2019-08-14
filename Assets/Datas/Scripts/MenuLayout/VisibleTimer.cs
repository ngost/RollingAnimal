using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibleTimer : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public float times;
    AudioSource audioSource;
    bool played=false;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        //for meshRender obj
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if(meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        //for have audio obj
        audioSource = gameObject.GetComponent<AudioSource>();


        //for text obj
        text = gameObject.GetComponent<Text>();
        if (text != null)
        {
            text.enabled = false;
//            Debug.Log(text.text);
        }

    }

    // Update is called once per frame
    void Update()
    {
        times -= Time.deltaTime;
        if (times < 0)
        {
            if(meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }

            if (text != null)
                text.enabled = true;

            if (audioSource != null && !played)
            {
                audioSource.Play();
                played = true;

            }
        }
    }
}
