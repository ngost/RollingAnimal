using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager instance = null;
    private static bool isAdsLoaded = false;
    private static bool isAdsLoadedForInterstitial = false;
    public string android_banner_id;
    public string ios_banner_id;
    public string android_rewarded_id;
    public string ios_rewarded_id;
    public string android_interstitial_id;
    public string ios_interstitial_id;
    public string android_coin_rewarded_id;
    string type;
    double amount;

    AdRequest request;
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardBasedVideoAd rewardBasedAd;
    private RewardBasedVideoAd coinRewardBasedAd;

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
            coinRewardBasedAd = RewardBasedVideoAd.Instance;
            RequestRewardBasedVideo();
            RequestCoinRewardBasedVideo();
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

        //adUnitId = "ca-app-pub-3940256099942544/1033173712"; //testId
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

        //adUnitId = "ca-app-pub-3940256099942544/5224354917"; // testId

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedAd.LoadAd(request, adUnitId);

        rewardBasedAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedAd.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

    }

    //보상형 광고 for coin
    private void RequestCoinRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = android_coin_rewarded_id;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        //adUnitId = "ca-app-pub-3940256099942544/5224354917"; // testID

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.coinRewardBasedAd.LoadAd(request, adUnitId);

        coinRewardBasedAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
        coinRewardBasedAd.OnAdClosed += HandleRewardBasedVideoClosed;
        coinRewardBasedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

    }


    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        type = SceneManager.GetActiveScene().name;
        switch (type)
        {
            case "ShopScene":
                SceneManager.LoadScene("GetCoinByAdScene");
                break;
            case "GreenRoomScene":
                LoadingSceneManager.LoadScene("Stage_" + StaticInfoManager.current_stage + "_" + (StaticInfoManager.level + 1));
                //print("User rewarded with: " + amount.ToString() + " " + type);
                break;
        }

    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        //RequestRewardBasedVideo();
//        if(args)
        rewardBasedAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedAd.OnAdClosed -= HandleRewardBasedVideoClosed;
        rewardBasedAd.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;

        coinRewardBasedAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        coinRewardBasedAd.OnAdClosed -= HandleRewardBasedVideoClosed;
        coinRewardBasedAd.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;

        RequestRewardBasedVideo();
        RequestCoinRewardBasedVideo();
        //        SimpleSceneFader.ChangeSceneWithFade("Stage_" + StaticInfoManager.current_stage + "_"+(StaticInfoManager.level+1));

    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        rewardBasedAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedAd.OnAdClosed -= HandleRewardBasedVideoClosed;
        rewardBasedAd.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;

        coinRewardBasedAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        coinRewardBasedAd.OnAdClosed -= HandleRewardBasedVideoClosed;
        coinRewardBasedAd.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;

        RequestRewardBasedVideo();
        RequestCoinRewardBasedVideo();

        if (SceneManager.GetActiveScene().name.Equals("GreenRoomScene"))
        {
            SSTools.ShowMessage(StaticInfoManager.lang.getString("adFail"), SSTools.Position.bottom, SSTools.Time.oneSecond);
            LoadingSceneManager.LoadScene("Stage_" + StaticInfoManager.current_stage + "_" + (StaticInfoManager.level + 1));
        }
        if (SceneManager.GetActiveScene().name.Equals("GreenRoomScene"))
        {
            SSTools.ShowMessage(StaticInfoManager.lang.getString("adFail"), SSTools.Position.bottom, SSTools.Time.oneSecond);
        }
    }

    //ad exit callback
    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        print("HandleOnInterstitialAdClosed event received.");

        interstitialAd.OnAdClosed -= HandleOnInterstitialAdClosed;
        interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        StaticInfoManager.life = 1;
        interstitialAd.Destroy();

        StaticInfoManager.current_player_position = 0f;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        SSTools.ShowMessage(StaticInfoManager.lang.getString("adFail"), SSTools.Position.bottom, SSTools.Time.oneSecond);
        interstitialAd.OnAdClosed -= HandleOnInterstitialAdClosed;
        interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        StaticInfoManager.life = 1;
        interstitialAd.Destroy();

        StaticInfoManager.current_player_position = 0f;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ShowRewardAd()
    {
        rewardBasedAd.Show();
    }

    public void ShowCoinRewardAd()
    {
        coinRewardBasedAd.Show();
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
