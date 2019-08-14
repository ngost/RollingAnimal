using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        SSTools.ShowMessage(StaticInfoManager.lang.getString("tutorial_sentence1"), SSTools.Position.top, SSTools.Time.threeSecond);
        StartCoroutine(NextSentence(StaticInfoManager.lang.getString("tutorial_sentence2"), 3f));
        StartCoroutine(NextSentence(StaticInfoManager.lang.getString("tutorial_sentence2"), 6f));
        StartCoroutine(NextSentence(StaticInfoManager.lang.getString("tutorial_sentence3"), 9f));
        DataLoadAndSave.SaveTutorialState();
        StaticInfoManager.current_stage = 0;
        StaticInfoManager.life = 100;

        var tutParams = new Dictionary<string, object>();
        tutParams[AppEventParameterName.ContentID] = "tutorial";
        tutParams[AppEventParameterName.Description] = "First step in the tutorial, Started Tutorial.";
        tutParams[AppEventParameterName.Success] = "1";

        FB.LogAppEvent(
            AppEventName.CompletedTutorial,
            parameters: tutParams
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > 17.2f && player.transform.position.z < 17.8f)
        {
            SSTools.ShowMessage(StaticInfoManager.lang.getString("tutorial_sentence4"), SSTools.Position.top, SSTools.Time.threeSecond);
        }

        if (player.transform.position.z > 29.2f && player.transform.position.z < 29.8f)
        {
            SSTools.ShowMessage(StaticInfoManager.lang.getString("tutorial_sentence5"), SSTools.Position.top, SSTools.Time.threeSecond);
        }

        if (player.transform.position.z > 33.2f && player.transform.position.z < 33.8f)
        {
            SSTools.ShowMessage(StaticInfoManager.lang.getString("tutorial_sentence6"), SSTools.Position.top, SSTools.Time.threeSecond);
        }
    }

    IEnumerator NextSentence(string msg, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SSTools.ShowMessage(msg, SSTools.Position.top, SSTools.Time.threeSecond);
    }
}
