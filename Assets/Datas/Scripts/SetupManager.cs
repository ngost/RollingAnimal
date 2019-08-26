using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System.IO;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading.Tasks;

public class SetupManager : MonoBehaviour
{
    // Awake function from Unity's MonoBehavior
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
        
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }


    void Start()
    {
        Lang langData;
        TextAsset textAsset = (TextAsset)Resources.Load<TextAsset>("LanguageSet/lang");
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Korean:
                langData = new Lang(textAsset.text, "Korean");
                Debug.Log("Language : Korean");
                break;
            case SystemLanguage.Ukrainian:
                langData = new Lang(textAsset.text, "Ukrainian");
                Debug.Log("Language : Ukrainian");
                break;
            case SystemLanguage.Russian:
                langData = new Lang(textAsset.text, "Russian");
                Debug.Log("Language : Russian");
                break;
            default:
                langData = new Lang(textAsset.text, "English");
                Debug.Log("Language : English");
                break;
        }
        StaticInfoManager.lang = langData;

        RetroPrinterScriptBasic menu_retro = (RetroPrinterScriptBasic)GameObject.Find("TouchText").GetComponent(typeof(RetroPrinterScriptBasic));
        menu_retro.CursorCharacter = StaticInfoManager.lang.getString("touch_text");

        GameDataLoader loader = new GameDataLoader();

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        GooglePlayServiceSetup();
    }
    public void GooglePlayServiceSetup()
    {
        StartAnotherScene startManager = (StartAnotherScene)GameObject.Find("SceneManager").GetComponent(typeof(StartAnotherScene));
        //구글 플레이 게임 서비스 초기

        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {

            PlayGamesPlatform.Instance.Authenticate((bool success) =>
            {
                //handle success or fail.
                if (success)
                {
                    SSTools.ShowMessage(Social.localUser.userName+"! "+StaticInfoManager.lang.getString("LoginSuccess"), SSTools.Position.bottom, SSTools.Time.twoSecond);
                    startManager.canTouchForTitle = true;
					Debug.Log("login");
                }
                else
                {
					Debug.Log("login fail");
					SSTools.ShowMessage(StaticInfoManager.lang.getString("LoginFail"), SSTools.Position.bottom, SSTools.Time.twoSecond);
                    StartCoroutine(QuitAfter3Second());

                    if (Application.platform == RuntimePlatform.OSXEditor)
                    {
                        startManager.canTouchForTitle = true;
                    }
                }
            });
        }
        else
        {
			Debug.Log("already login");
			SSTools.ShowMessage(Social.localUser.userName + "! " + StaticInfoManager.lang.getString("LoginSuccess"), SSTools.Position.bottom, SSTools.Time.twoSecond);
            startManager.canTouchForTitle = true;
        }


        		
    }

    IEnumerator QuitAfter3Second()
    {
        yield return new WaitForSeconds(3f);
        if (Application.platform == RuntimePlatform.Android)
        {
            new AndroidJavaClass("java.lang.System").CallStatic("exit", 0);
        }
        Application.Quit();

    }
}
