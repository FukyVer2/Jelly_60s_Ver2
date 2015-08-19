using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;


public class GoogleAds_J60s : MonoSingleton<GoogleAds_J60s> {

    private BannerView bannerView;
    private InterstitialAd interstitial;

    private string android_Banner_BlueBird = "ca-app-pub-8112894826901791/5149068463";

    private string iOS_Banner_BlueBird = "ca-app-pub-8112894826901791/9718868865";

    private string android_FullScreen_BlueBird = "ca-app-pub-8112894826901791/6625801664";

    private string iOS_FullScreen_BlueBird = "ca-app-pub-8112894826901791/2195602065";

    private string android_Banner_AdsID = "ca-app-pub-8185609774740638/7070835702";
    private string android_FullScreen_AdsID = "ca-app-pub-8185609774740638/8547568901";
    private string iOS_Banner_AdsID = "ca-app-pub-8185609774740638/9884701303";
    private string iOS_FullScreen_AdsID = "ca-app-pub-8185609774740638/2361434500";

    bool isLoadBanner;

	// Use this for initialization
	void Start () {
        Debug.Log("Google Ads");
        isLoadBanner = false;
        SetAdsID();
#if UNITY_ANDROID || UNITY_IOS
		RequestBanner();
		RequestInterstitial();
		
		GoogleAds_J60s.Instance.ShowBanner();
#endif
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
  

    void SetAdsID()
    {       
        android_Banner_AdsID = android_Banner_BlueBird;
        android_FullScreen_AdsID = android_FullScreen_BlueBird;

        iOS_Banner_AdsID = iOS_Banner_BlueBird;
        iOS_FullScreen_AdsID = iOS_FullScreen_BlueBird;        
    }

    public void RequestFullScreen()
    {
        if (!interstitial.IsLoaded())
        {
            RequestInterstitial();
        }
    }

    public void RequestMyBanner()
    {
        if (!isLoadBanner)
        {
            RequestBanner();
        }
    }

    public void DestroyFullScreen()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Destroy();
        }

    }

    /// <summary>
    /// Kiem tra xem interstitial Ads da load xong chua?
    /// </summary>
    /// <returns></returns>
    public bool ReadyLoadFullScreen()
    {
        return interstitial.IsLoaded();
    }

    /// <summary>
    /// Hien thi full-screen ads.
    /// </summary>
    public void ShowFullScreen()
    {
        if (interstitial.IsLoaded())
        {
            ShowInterstitial();
        }
        else
        {
            RequestFullScreen();
        }
    }

    /// <summary>
    /// Hien thi banner ads.
    /// </summary>
    public void ShowBanner()
    {
        if (isLoadBanner)
        {            
            bannerView.Show();
        }
        else
        {
            
            RequestBanner();
        }
    }

    /// <summary>
    /// Tat banner ads.
    /// </summary>
    /// 

    public void HideBanner()
    {
        bannerView.Hide();
    }

    /// <summary>
    /// Destroy banner
    /// </summary>
    public void DestroyBanner()
    {
        bannerView.Destroy();
        isLoadBanner = false;
    }
    private void RequestBanner()
    {

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = android_Banner_AdsID;//"ca-app-pub-8185609774740638/7070835702";
#elif UNITY_IOS
		string adUnitId = iOS_Banner_AdsID;//"ca-app-pub-8185609774740638/9884701303";
#else
            string adUnitId = "unexpected_platform";
#endif
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        // Register for ad events.
        bannerView.AdLoaded += HandleAdLoaded;
        bannerView.AdFailedToLoad += HandleAdFailedToLoad;
        bannerView.AdOpened += HandleAdOpened;
        bannerView.AdClosing += HandleAdClosing;
        bannerView.AdClosed += HandleAdClosed;
        bannerView.AdLeftApplication += HandleAdLeftApplication;
        // Load a banner ad.
        bannerView.LoadAd(createAdRequest());

    }
    private void RequestInterstitial()
    {

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = android_FullScreen_AdsID;//"ca-app-pub-8185609774740638/8547568901";
#elif UNITY_IOS
		string adUnitId = iOS_FullScreen_AdsID;//"ca-app-pub-8185609774740638/2361434500";
#else
            string adUnitId = "unexpected_platform";
#endif
        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        // Register for ad events.
        interstitial.AdLoaded += HandleInterstitialLoaded;
        interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.AdOpened += HandleInterstitialOpened;
        interstitial.AdClosing += HandleInterstitialClosing;
        interstitial.AdClosed += HandleInterstitialClosed;
        interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
        // Load an interstitial ad.
        interstitial.LoadAd(createAdRequest());
    }
    // Returns an ad request with custom ad targeting.
    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            //                .AddKeyword("game")
            //                .SetGender(Gender.Male)
            //                .SetBirthday(new DateTime(1985, 1, 1))
            //                .TagForChildDirectedTreatment(false)
            //                .AddExtra("color_bg", "9B30FF")
                .Build();

    }


    void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {

            print("Interstitial is not ready yet.");
        }
    }

    #region Banner callback handlers

    void HandleAdLoaded(object sender, EventArgs args)
    {
        isLoadBanner = true;
        print("HandleAdLoaded event received.");
    }

    void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isLoadBanner = false;
        print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    void HandleAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    void HandleAdClosing(object sender, EventArgs args)
    {
        //isLoadBanner = false;
        print("HandleAdClosing event received");
    }

    void HandleAdClosed(object sender, EventArgs args)
    {
        //isLoadBanner = false;
        print("HandleAdClosed event received");
    }

    void HandleAdLeftApplication(object sender, EventArgs args)
    {
        print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("HandleInterstitialLoaded event received.");
    }

    void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("HandleInterstitialOpened event received");
    }

    void HandleInterstitialClosing(object sender, EventArgs args)
    {
        print("HandleInterstitialClosing event received");
    }

    void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("HandleInterstitialClosed event received");
    }

    void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        print("HandleInterstitialLeftApplication event received");
    }

    #endregion
}
