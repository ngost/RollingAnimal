using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyorForDonDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject audio = GameObject.Find("AudioManager");
        if (audio !=null)
            Destroy(audio);
    }
    
}
