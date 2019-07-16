using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager instance;
    private static bool isAdsLoaded = false;
    private static bool isAdsLoadedForInterstitial = false;
    public string android_banner_id;
    public string ios_banner_id;
    public string android_rewarded_id;
    public string ios_rewarded_id;
    public string android_interstitial_id;
    public string ios_interstitial_id;

    AdRequest request;
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardBasedVideoAd rewardBasedAd;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
            MobileAds.Initialize(android_rewarded_id);
            rewardBasedAd = RewardBasedVideoAd.Instance;
            RequestRewardBasedVideo();

            //MobileAds.Initialize(android_rewarded_id);
//            interstitialAd = new InterstitialAd(adUnitId); ;
            RequestInterstitialAd();

        //RequestBannerAd();

        //        RequestInterstitialAd();


        //ShowBannerAd();
    }
 

    //배너 광고
    public void RequestBannerAd()
    {
        string adUnitId = string.Empty;
 
#if UNITY_ANDROID
        adUnitId = android_banner_id;
#elif UNITY_IOS
        adUnitId = ios_bannerAdUnitId;
#endif
 
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
 
        bannerView.LoadAd(request);
    }
 
    //전면 광고
    private void RequestInterstitialAd()
    {
        string adUnitId = string.Empty;
 
#if UNITY_ANDROID
        adUnitId = android_interstitial_id;
#elif UNITY_IOS
        adUnitId = ios_interstitialAdUnitId;
#endif
 
        interstitialAd = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
 
        interstitialAd.LoadAd(request);
 
        interstitialAd.OnAdClosed += HandleOnInterstitialAdClosed;
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
    }

    //보상형 광고
    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = android_rewarded_id;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif



        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedAd.LoadAd(request, adUnitId);

        rewardBasedAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedAd.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;


    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print("User rewarded with: " + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        //RequestRewardBasedVideo();
        rewardBasedAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedAd.OnAdClosed -= HandleRewardBasedVideoClosed;
        rewardBasedAd.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
        SimpleSceneFader.ChangeSceneWithFade("Stage_" + StaticInfoManager.current_stage + "_"+(StaticInfoManager.level+1));

    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        SSTools.ShowMessage("보여 드릴 광고가 없습니다.", SSTools.Position.bottom, SSTools.Time.oneSecond);
        rewardBasedAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedAd.OnAdClosed -= HandleRewardBasedVideoClosed;
        rewardBasedAd.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
        SimpleSceneFader.ChangeSceneWithFade("Stage_" + StaticInfoManager.current_stage + "_" + (StaticInfoManager.level + 1));
    }

    //ad exit callback
    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        print("HandleOnInterstitialAdClosed event received.");

        interstitialAd.OnAdClosed -= HandleOnInterstitialAdClosed;
        interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        StaticInfoManager.life = 1;
        interstitialAd.Destroy();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        SSTools.ShowMessage("보여 드릴 광고가 없습니다.", SSTools.Position.bottom, SSTools.Time.oneSecond);
        interstitialAd.OnAdClosed -= HandleOnInterstitialAdClosed;
        interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        StaticInfoManager.life = 1;
        interstitialAd.Destroy();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ShowRewardAd()
    {
        rewardBasedAd.Show();
    }

    public void ShowBannerAd()
    {
        bannerView.Show();
    }
 
    public void ShowInterstitialAd()
    {
        if (!interstitialAd.IsLoaded())
        {
            RequestInterstitialAd();
            return;
        }
 
        interstitialAd.Show();
    }
 
}
