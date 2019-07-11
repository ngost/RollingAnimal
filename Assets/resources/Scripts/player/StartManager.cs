using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartManager : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            player.transform.position = StaticInfoManager.last_checkpoint;
        }
        catch (UnassignedReferenceException e)
        {
            StaticInfoManager.last_checkpoint = new Vector3(0f, 0f, 0f);
            player.transform.position = StaticInfoManager.last_checkpoint;
        }


    }
}
