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
        SSTools.ShowMessage("반갑습니다. 롤링 애니멀은 처음이시죠?", SSTools.Position.top, SSTools.Time.threeSecond);
        StartCoroutine(NextSentence("발자국을 따라 이동해보세요.",3f));
        StartCoroutine(NextSentence("발자국을 따라 이동해보세요.",6f));
        StartCoroutine(NextSentence("잘하고 있어요. 계속 이동해보세요.",9f));
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
            SSTools.ShowMessage("앞에 장애물이 있습니다. 피해서 건너가세요.", SSTools.Position.top, SSTools.Time.threeSecond);
        }

        if (player.transform.position.z > 29.2f && player.transform.position.z < 29.8f)
        {
            SSTools.ShowMessage("거의 다 왔어요. 힘내세요!", SSTools.Position.top, SSTools.Time.threeSecond);
        }

        if (player.transform.position.z > 33.2f && player.transform.position.z < 33.8f)
        {
            SSTools.ShowMessage("축하합니다! 튜토리얼을 완료하셨습니다.", SSTools.Position.top, SSTools.Time.threeSecond);
        }
    }

    IEnumerator NextSentence(string msg, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SSTools.ShowMessage(msg, SSTools.Position.top, SSTools.Time.threeSecond);
    }
}
