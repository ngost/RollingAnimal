using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppFlyerInitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /* Mandatory - set your AppsFlyer’s Developer key. */
        AppsFlyer.setAppsFlyerKey("zcKrZYJWnrWWctCxcLNnyT");
        /* For detailed logging */
        /* AppsFlyer.setIsDebug (true); */
        #if UNITY_IOS
           /* Mandatory - set your apple app ID
              NOTE: You should enter the number only and not the "ID" prefix */
           AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
           AppsFlyer.trackAppLaunch ();
        #elif UNITY_ANDROID
                /* Mandatory - set your Android package name */
                AppsFlyer.setAppID("com.easyjin.rollinganimal");
                /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
                AppsFlyer.init("zcKrZYJWnrWWctCxcLNnyT", "AppsFlyerTrackerCallbacks");
        #endif



        #if !UNITY_EDITOR
                AppLovin.InitializeSdk();
        #endif
    }
}