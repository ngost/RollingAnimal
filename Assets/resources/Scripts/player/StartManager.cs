using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Facebook.Unity;
using UnityEngine.SceneManagement;

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

        var tryParams = new Dictionary<string, object>();
        tryParams[AppEventParameterName.ContentID] = SceneManager.GetActiveScene().name+ " was challenged.";
        tryParams[AppEventParameterName.Description] = "Specific Stage("+SceneManager.GetActiveScene().name+ ") was Challenged by some player.";
        tryParams[AppEventParameterName.Success] = "1";

        FB.LogAppEvent(
                    AppEventName.ViewedContent,
                    parameters: tryParams
                );
    }
}
