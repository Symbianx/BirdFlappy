using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdController
{
    private InterstitialAd interstitial;
    private int gamesSinceLastAd = 0;
    public void BottomBanner()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-7419541983974774/6749567775";
        #elif UNITY_IPHONE
                    string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
        #else
                    string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void CreateInterstitialBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-7419541983974774/8086700172";
        #elif UNITY_IPHONE
            string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
        interstitial.AdClosed += interstitial_AdClosed;
    }

    void interstitial_AdClosed(object sender, System.EventArgs e)
    {
        ReloadInterstitialBanner();
    }

    public void ReloadInterstitialBanner()
    {
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void SetEvents(GameController gc)
    {
        GameObject.Find("Bird").GetComponent<BirdController>().GameOver += AdController_GameOver;
        gc.GameStarted += GameController_GameStarted;
        CreateInterstitialBanner();
    }

    void GameController_GameStarted(object sender, System.EventArgs e)
    {
        //if (gamesSinceLastAd == 0 || gamesSinceLastAd > 5)
        //{
        //}
    }

    void AdController_GameOver(object sender, System.EventArgs e)
    {
        if (gamesSinceLastAd == 0 || gamesSinceLastAd > 5)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
                //ReloadInterstitialBanner();
                gamesSinceLastAd = 0;
            }
        }
        gamesSinceLastAd++;
    }
}
