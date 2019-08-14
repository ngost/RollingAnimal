using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    public static AssetBundle bundle;
    GameObject audioManager;
	GameDataLoader loader;
	InventoryClass inventory;
    UnityWebRequest uwr;
    String[] urls;

    [SerializeField]
    Image progressBar;
    [SerializeField]
    Image ProgressBarForDownloading;

    
    string nextSceneName;
    bool isDownloading = true;
    string[] stage_number;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        

        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();
        audioManager = GameObject.Find("AudioManager");
        urls = new string[10];
        urls[4] = "https://www.dropbox.com/s/ovs2ma5nmuljm7s/stage4_bundle?dl=1";
        urls[5] = "https://www.dropbox.com/s/y2o99y974kq6rt2/stage5_bundle?dl=1";
        //check using item
        CheckUsingItem();

        stage_number = nextScene.Split('_');


        if (int.Parse(stage_number[1]) >= 4)
        {
            #if UNITY_EDITOR_OSX
                StartCoroutine(LoadScene());
                return;
            #endif
            StartCoroutine(LoadFromHosting(urls[int.Parse(stage_number[1])]));
        }
        else
        {
            StartCoroutine(LoadScene());
        }
        //load scene


    }

    
    public void CheckUsingItem()
	{
		loader = new GameDataLoader();
		inventory = loader.LoadData();
        if (DataLoadAndSave.LoadShieldItemIsUsing().Equals(1))
        {
            if (inventory.shieldItem > 0)
            {
                inventory.shieldItem = inventory.shieldItem - 1;
                StaticInfoManager.shieldEnable = true;
            }
            else
            {
                StaticInfoManager.shieldEnable = false;
            }
        }
        else
        {
            StaticInfoManager.shieldEnable = false;
        }


        if (DataLoadAndSave.LoadFeverItemIsUsing().Equals(1))
        {
            if (inventory.feverItem > 0)
            {
                inventory.feverItem = inventory.feverItem - 1;
                StaticInfoManager.feverEnable = true;
            }
            else
            {
                StaticInfoManager.feverEnable = false;
            }
        }
        else
        {
            StaticInfoManager.feverEnable = false;
        }

        if (DataLoadAndSave.LoadRewardItemIsUsing().Equals(1))
        {
            if (inventory.rewardItem > 0)
            {
                inventory.rewardItem = inventory.rewardItem - 1;
                StaticInfoManager.rewardEnable = true;
            }
            else
            {
                StaticInfoManager.rewardEnable = false;
            }
        }
        else
        {
            StaticInfoManager.rewardEnable = false;
        }

        loader.WriteData(inventory);
    }

    

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.2f) ;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {

                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    Destroy(audioManager);
                    yield return new WaitForSeconds(0.5f);
                    op.allowSceneActivation = true;
                }

            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
        }
    }

    IEnumerator LoadFromHosting(string url)
    {
        int downloaded = 0;
        bundle = null;

        while (!Caching.ready)
            yield return null;

        using (uwr = UnityWebRequestAssetBundle.GetAssetBundle(url, 2, 0))
        {

            StartCoroutine(DownloadProgressing());

            yield return uwr.SendWebRequest();
            isDownloading = false;

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                bundle = DownloadHandlerAssetBundle.GetContent(uwr);

                string[] scenePaths = bundle.GetAllScenePaths();
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[int.Parse(stage_number[2]) - 1]);
                nextScene = sceneName;
                Debug.Log("nextScene : " + nextScene);

                StartCoroutine(LoadScene());
                //                SceneManager.LoadScene(sceneName);

                //bundle.Unload(false);
            }
        }
    }

    IEnumerator DownloadProgressing()
    {
        while (isDownloading)
        {
            yield return new WaitForSeconds(0.1f);
            try
            {
                if (uwr.downloadProgress != null)
                    ProgressBarForDownloading.fillAmount = uwr.downloadProgress;
            }
            catch(ArgumentNullException e)
            {
                ProgressBarForDownloading.fillAmount = 1;
            }
            


        }
        
    }
}