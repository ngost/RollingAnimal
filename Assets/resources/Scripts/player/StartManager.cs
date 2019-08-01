using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject player;
    private string[] charactor_string;
    // Start is called before the first frame update
    void Start()
    {
        int index = DataLoadAndSave.LoadSelectedCharator();
        charactor_string = new string[22];
        charactor_string[0] = "Dog";
        charactor_string[1] = "Duck";
        charactor_string[2] = "Chicken";
        charactor_string[3] = "Chick";
        charactor_string[4] = "Sheep";
        charactor_string[5] = "Goat";
        charactor_string[6] = "Bunny";
        charactor_string[7] = "Cat";
        charactor_string[8] = "Pig";
        charactor_string[9] = "Cow";
        charactor_string[10] = "Deer";
        charactor_string[11] = "Turtle";
        charactor_string[12] = "Lizard";
        charactor_string[13] = "Wolf";
        charactor_string[14] = "Fox";
        charactor_string[15] = "Bird";
        charactor_string[16] = "Bear";
        charactor_string[17] = "Horse";
        charactor_string[18] = "Zebra";
        charactor_string[19] = "Panda";
        charactor_string[20] = "Owl";
        charactor_string[21] = "Lion";

        GameObject actor = player.transform.Find("actor").gameObject;
        GameObject charater = (GameObject)Instantiate(Resources.Load("prefabs/character/" + charactor_string[index]));
        (charater.GetComponent<ItemRoator>()).enabled = false;
        //자식의 부모 지정 코드
        charater.transform.parent = actor.transform;
        charater.transform.position = new Vector3(0f, 0.5f, 0f);
        charater.transform.rotation = Quaternion.Euler(0, 180f, 0);
        charater.transform.localScale = new Vector3(1f, 1f, 1f);

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
