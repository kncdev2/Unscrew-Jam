// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using DG.Tweening;
// #if UNITY_EDITOR
// using AppLovinMax.Scripts.IntegrationManager.Editor;
// #endif
// using Fenj.Common;
// using Firebase.Analytics;
// using GoogleMobileAds.Ump.Api;
// //using GoogleMobileAds.Ump.Api;
// using Sirenix.OdinInspector;
// using UnityEngine;

// public class AdManager : Singleton<AdManager>
// {
// #if UNITY_ANDROID
//     const string MaxSdkKey = "rfRkAV38zCy_y59ckoZKWRyLuPvtl_qvoEm5VzpdpecP0VUbN953ul7nhx0u71wgBkpDu2PTdF8_4x8THZC0FI";
//     const string BannerAdUnitId = "537513803e5a68d5";
//     const string InterstitialAdUnitId = "3f3cd582bbd842e2";
//     const string RewardedAdUnitId = "b32f86692fbaa69f";
//     private const string AppOpenAdUnitId = "2257ff0e202d9696";
// #elif UNITY_IPHONE || UNITY_IOS
//     const string MaxSdkKey = "rfRkAV38zCy_y59ckoZKWRyLuPvtl_qvoEm5VzpdpecP0VUbN953ul7nhx0u71wgBkpDu2PTdF8_4x8THZC0FI";
//     const string InterstitialAdUnitId = "7814e1753d22d41b";
//     const string RewardedAdUnitId = "8ac0b430403da582";
//     const string BannerAdUnitId = "7aeb2b53fb2e49a3";
//     private const string AppOpenAdUnitId = "81093e910526eeca";
// #else 
//     const string MaxSdkKey = "rfRkAV38zCy_y59ckoZKWRyLuPvtl_qvoEm5VzpdpecP0VUbN953ul7nhx0u71wgBkpDu2PTdF8_4x8THZC0FI";
//     const string InterstitialAdUnitId = "e6eb4c0122fd31dc";
//     const string RewardedAdUnitId = "2c28a5672eee34ba";
//     const string BannerAdUnitId = "46b4979cc213d013";
// #endif

//     private int interstitialRetryAttempt;
//     private int rewardedRetryAttempt;
//     private int rewardedInterstitialRetryAttempt;

//     private Action onRewardSuccess;

//     private Dictionary<string, InterstitialCondition> interstitialConditions = new Dictionary<string,InterstitialCondition>();

//     private const string IsRemoveAdsKey = "IsRemoveAdsKey";

//     public static bool CanShowInter => config.canShowInter;
//     public static bool CanShowOpenAds => config.canShowOpenAds;

//     public class AdConfig
//     {
//         public bool canShowInter = true;
//         public bool canShowOpenAds = true;
//         public float timeCappingInter = 15f;
//         public bool canShowBanner = true;
//     }

//     public static AdConfig config;
//     public static bool IsRemoveAds
//     {
//         set => PlayerPrefs.SetInt(IsRemoveAdsKey, value ? 1 : 0);
//         get => PlayerPrefs.GetInt(IsRemoveAdsKey, 0) == 1;
//     }

    
//     // void Start()
//     // {
//     //     // Set tag for under age of consent.
//     //     // Here false means users are not under age of consent.
//     //     ConsentRequestParameters request = new ConsentRequestParameters
//     //     {
//     //         TagForUnderAgeOfConsent = false,
//     //     };
//     //
//     //     // Check the current consent information status.
//     //     ConsentInformation.Update(request, OnConsentInfoUpdated);
//     // }
//     //
//     // void OnConsentInfoUpdated(FormError consentError)
//     // {
//     //     if (consentError != null)
//     //     {
//     //         // Handle the error.
//     //         UnityEngine.Debug.LogError(consentError);
//     //         return;
//     //     }
//     //
//     //     // If the error is null, the consent information state was updated.
//     //     // You are now ready to check if a form is available.
//     // }
//     void OnConsentInfoUpdated(FormError consentError)
//     {
//         if (consentError != null)
//         {
//             // Handle the error.
//             UnityEngine.Debug.LogError(consentError);
//             return;
//         }
//         ConsentForm.LoadAndShowConsentFormIfRequired((FormError formError) =>
//         {
//             if (formError != null)
//             {
//                 // Consent gathering failed.
//                 UnityEngine.Debug.LogError(consentError);
//                 return;
//             }

//             // Consent has been gathered.
//             if (ConsentInformation.CanRequestAds())
//             {
//                 InitAds();
//             }
//         });
//         // If the error is null, the consent information state was updated.
//         // You are now ready to check if a form is available.
//     }
//     public void Init()
//     {
// #if UNITY_IOS
//         if (ConsentInformation.PrivacyOptionsRequirementStatus == PrivacyOptionsRequirementStatus.Required)
//         {
//             // Create a ConsentRequestParameters object.
//             ConsentRequestParameters request = new ConsentRequestParameters();

//             // Check the current consent information status.
//             ConsentInformation.Update(request, OnConsentInfoUpdated);
//         }
//         else
//         {
//             InitAds();
//         }


// #endif
//         // InitAds();

// // #if UNITY_ANDROID
// //         InitAds();
// // #endif
//         // if (ConsentInformation.PrivacyOptionsRequirementStatus == PrivacyOptionsRequirementStatus.Required)
//         // {
//         //     ConsentRequestParameters request = new ConsentRequestParameters
//         //     {
//         //         TagForUnderAgeOfConsent = false,
//         //     };
//         //
//         //     // Check the current consent information status.
//         //     ConsentInformation.Update(request, delegate(FormError consentError)
//         //     {
//         //         InitAds();
//         //         if (consentError != null)
//         //         {
//         //             // Handle the error.
//         //             UnityEngine.Debug.LogError(consentError);
//         //             return;
//         //         }
//         //     });
//         // }
//         // else
//         // {
//         //     InitAds();
//         // }
//     }

//     void InitAds()
//     {
//         if (ApplicationSetting.Instance.isNoAds)
//         {
//             return;
//         }
//         DontDestroyOnLoad(gameObject);
//         MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
//         {
//             // AppLovin SDK is initialized, configure and start loading ads.
//             Debug.Log("MAX SDK Initialized");
//             InitializeInterstitialAds();
//             InitializeRewardedAds();
//             InitializeBannerAds();
//             InitAOA();
//             isInit = true;
//             // Initialize Adjust SDK
//         };

//         MaxSdk.SetSdkKey(MaxSdkKey);
//         MaxSdk.InitializeSdk();
//     }



//     public bool isInit;

//     [ReadOnly]
//     [ShowInInspector]
//     private float cdAuto = 30;

//     [ReadOnly]
//     [ShowInInspector]
//     private float rateAuto = 30;

//     private float cappingTime;
//     public bool IsShowAds;


//     // private void Update()
//     // {
//     //     if (rateAuto > 0)
//     //     {
//     //         rateAuto -= Time.deltaTime;
//     //     }
//     //     else
//     //     {
//     //         UIManager.Instance.ShowPopup<LoadingAds>();
//     //         var currentTime = Time.timeScale;
//     //         Time.timeScale = 0;
//     //         DOVirtual.DelayedCall(0.5f, delegate
//     //         {
//     //             ShowInterstitial("Auto Show");
//     //         });
//     //         DOVirtual.DelayedCall(1, delegate
//     //         {
//     //             Time.timeScale = 1;
//     //             UIManager.Instance.GetPopup<LoadingAds>().Hide();
//     //         },true);
//     //         ResetTime();
//     //     }
//     // }

//     private void Update()
//     {
//         if (cappingTime > 0)
//         {
//             cappingTime -= Time.deltaTime;
//         }
//     }

//     void ResetTime()
//     {
//         rateAuto = cdAuto;
//         cappingTime = config.timeCappingInter;
//     }

//     #region Interstitial Ad Methods

//     private void InitializeInterstitialAds()
//     {
//         // Attach callbacks
//         MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
//         MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
//         MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += InterstitialFailedToDisplayEvent;
//         MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
//         MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;
        
//         // Load the first interstitial
//         LoadInterstitial();
//     }

//     void LoadInterstitial()
//     {
//         MaxSdk.LoadInterstitial(InterstitialAdUnitId);
//     }

//     public void ShowInterstitial(string placement)
//     {
//         if (ApplicationSetting.Instance.isNoAds || IsRemoveAds  || !CanShowInter || cappingTime > 0)
//         {
//             return;
//         }

//         try
//         {
//             FirebaseAnalytics.LogEvent("inter_attempt");
//         }
//         catch (Exception e)
//         {
//             Debug.Log(e);
//         }
//         if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
//         {
//             IsShowAds = true;
//             canShowAOA = false;
//             MaxSdk.ShowInterstitial(InterstitialAdUnitId,placement);
//             FirebaseAnalytics.LogEvent("Interstitial_show",new Parameter("placement",placement));
//         }
//         else
//         {
//             Debug.Log("interstitial not ready");
//         }
//     }   
//     public void ShowInterstitial(string placement,int target)
//     {
//         if (interstitialConditions.ContainsKey(placement))
//         {
//             interstitialConditions[placement].ShowInter();
//         }
//         else
//         {
//             interstitialConditions.Add(placement,new InterstitialCondition(placement,target));
//             interstitialConditions[placement].ShowInter();
//         }
//     }
    
    
//     private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
//         Debug.Log("Interstitial loaded");
        
//         // Reset retry attempt
//         interstitialRetryAttempt = 0;
//     }

//     private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
//     {
//         // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
//         interstitialRetryAttempt++;
//         double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));
        
//         Debug.Log("Load failed: " + errorInfo.Code + "\nRetrying in " + retryDelay + "s...");
//         Debug.Log("Interstitial failed to load with error code: " + errorInfo.Code);
        
//         Invoke("LoadInterstitial", (float) retryDelay);
//     }

//     private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
//     {
//         // Interstitial ad failed to display. We recommend loading the next ad
//         Debug.Log("Interstitial failed to display with error code: " + errorInfo.Code);
//         LoadInterstitial();
//     }

//     private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         // Interstitial ad is hidden. Pre-load the next ad
//         Debug.Log("Interstitial dismissed");
//         LoadInterstitial();
//         FirebaseAnalytics.LogEvent("af_inters");
//         //Adjust.trackEvent(new AdjustEvent(EventAdjust.aj_inter));
//         ResetTime();
//         IsShowAds = false;
//         canShowAOA = true;
//     }

//     private void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         // Interstitial ad revenue paid. Use this callback to track user revenue.
//         Debug.Log("Interstitial revenue paid");

//         // Ad revenue
//         double revenue = adInfo.Revenue;
        
//         // Miscellaneous data
//         string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
//         string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
//         string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
//         string placement = adInfo.Placement; // The placement this ad's postbacks are tied to
        
//         TrackAdRevenue(adInfo);
//     }

//     #endregion

//     #region Rewarded Ad Methods

//     private void InitializeRewardedAds()
//     {
//         // Attach callbacks
//         MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
//         MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
//         MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
//         MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
//         MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
//         MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
//         MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
//         MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;

//         // Load the first RewardedAd
//         LoadRewardedAd();
//     }

//     private void LoadRewardedAd()
//     {
//         MaxSdk.LoadRewardedAd(RewardedAdUnitId);
//     }

//     public void ShowRewardedAd(string placement,Action onComplete)
//     {
//         if (ApplicationSetting.Instance.isNoAds)
//         {
//             onComplete?.Invoke();
//             return;
//         }

//         try
//         {
//             FirebaseAnalytics.LogEvent("reward_attempt");
//         }
//         catch (Exception e)
//         {
//             Debug.Log(e);
//         }
        
//         if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
//         {
//             onRewardSuccess = onComplete;
//             canShowAOA = false;
//             MaxSdk.ShowRewardedAd(RewardedAdUnitId,placement);
//             FirebaseAnalytics.LogEvent("rewarded_video_show",new Parameter("placement",placement));
//             ResetTime();
//         }
//         else
//         {
//             Debug.Log("Rewarded Ad not ready");
//         }
//     }

//     private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'
//         Debug.Log("Rewarded ad loaded");
        
//         // Reset retry attempt
//         rewardedRetryAttempt = 0;
//     }

//     private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
//     {
//         // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
//         rewardedRetryAttempt++;
//         double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));
        
//         Debug.Log("Load failed: " + errorInfo.Code + "\nRetrying in " + retryDelay + "s...");
//         Debug.Log("Rewarded ad failed to load with error code: " + errorInfo.Code);
        
//         Invoke("LoadRewardedAd", (float) retryDelay);
//     }

//     private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
//     {
//         // Rewarded ad failed to display. We recommend loading the next ad
//         Debug.Log("Rewarded ad failed to display with error code: " + errorInfo.Code);
//         LoadRewardedAd();
//         IsShowAds = false;
//     }

//     private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         Debug.Log("Rewarded ad displayed");
//         FirebaseAnalytics.LogEvent("af_reward");
//         //Adjust.trackEvent(new AdjustEvent(EventAdjust.aj_reward));
//         IsShowAds = true;

//     }

//     private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         Debug.Log("Rewarded ad clicked");
//     }

//     private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         // Rewarded ad is hidden. Pre-load the next ad
//         Debug.Log("Rewarded ad dismissed");
//         LoadRewardedAd();
//         IsShowAds = false;
//         canShowAOA = true;
//     }

//     private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
//     {
//         // Rewarded ad was displayed and user should receive the reward
//         onRewardSuccess?.Invoke();
//         onRewardSuccess = null;
//         Debug.Log("Rewarded ad received reward");
//     }

//     private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         // Rewarded ad revenue paid. Use this callback to track user revenue.
//         Debug.Log("Rewarded ad revenue paid");

//         // Ad revenue
//         double revenue = adInfo.Revenue;
        
//         // Miscellaneous data
//         string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
//         string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
//         string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
//         string placement = adInfo.Placement; // The placement this ad's postbacks are tied to
        
//         TrackAdRevenue(adInfo);
//     }

//     #endregion
    
//     #region Banner Ad Methods

//     private void InitializeBannerAds()
//     {
//         if (!config.canShowBanner)
//         {
//             return;
//         }
//         // Attach Callbacks
//         MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
//         MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdFailedEvent;
//         MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
//         MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;

//         // Banners are automatically sized to 320x50 on phones and 728x90 on tablets.
//         // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.
//         MaxSdk.CreateBanner(BannerAdUnitId, MaxSdkBase.BannerPosition.TopRight);

//         // Set background or background color for banners to be fully functional.
//         // MaxSdk.SetBannerBackgroundColor(BannerAdUnitId, Color.black);
//         MaxSdk.SetBannerBackgroundColor(BannerAdUnitId,Color.clear);
//         ShowBanner();
//     }

//     public void ShowBanner()
//     {
//         if (!IsRemoveAds && config.canShowBanner)
//         {
//             MaxSdk.ShowBanner(BannerAdUnitId);
//         }
//     }

//     public void HideBanner()
//     {
//         MaxSdk.HideBanner(BannerAdUnitId);
//     }
//     private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         // Banner ad is ready to be shown.
//         // If you have already called MaxSdk.ShowBanner(BannerAdUnitId) it will automatically be shown on the next ad refresh.
//         Debug.Log("Banner ad loaded");
//     }

//     private void OnBannerAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
//     {
//         // Banner ad failed to load. MAX will automatically try loading a new ad internally.
//         Debug.Log("Banner ad failed to load with error code: " + errorInfo.Code);
//     }

//     private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         Debug.Log("Banner ad clicked");
//     }

//     private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         // Banner ad revenue paid. Use this callback to track user revenue.
//         Debug.Log("Banner ad revenue paid");

//         // Ad revenue
//         double revenue = adInfo.Revenue;
        
//         // Miscellaneous data
//         string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
//         string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
//         string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
//         string placement = adInfo.Placement; // The placement this ad's postbacks are tied to
        
//         TrackAdRevenue(adInfo);
//     }

//     #endregion
    
//     #region AOA Ad Methods

//     private bool canShowAOA = true;
//     private void OnApplicationPause(bool pauseStatus)
//     {
//         if (!pauseStatus && canShowAOA && isInit)
//         {
//             ShowAOAIfReady();
//         }
//     }
//     public void InitAOA()
//     {
//         MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenDismissedEvent;
//         MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += AppOpenOnOnAdLoadedEvent;
//         MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += AppOpenOnOnAdLoadFailedEvent;
//         MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent += AppOpenOnOnAdDisplayedEvent;
//         MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
//     }
//     private void AppOpenOnOnAdDisplayedEvent(string arg1, MaxSdkBase.AdInfo arg2)
//     {
//         Debug.Log("show aoa");
//     }

//     private void AppOpenOnOnAdLoadFailedEvent(string arg1, MaxSdkBase.ErrorInfo arg2)
//     {
//         Debug.Log("load aoa fail");
//     }

//     private void AppOpenOnOnAdLoadedEvent(string arg1, MaxSdkBase.AdInfo arg2)
//     {
//         Debug.Log("load aoa success");
//     }

//     public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
//     {
//         MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
//     }

//     public bool IsAOAReady()
//     {
//         return MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId);
//     }
//     public void ShowAOAIfReady()
//     {
//         if (!CanShowOpenAds)
//         {
//             return;
//         }
//         if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
//         {
//             MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
//         }
//         else
//         {
//             MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
//         }
//     }

//     #endregion
    
//     private void TrackAdRevenue(MaxSdkBase.AdInfo adInfo)
//     {
//         // Interstitial ad revenue paid. Use this callback to track user revenue.
//         Debug.Log(" revenue paid");

//         // Ad revenue
//         double revenue = adInfo.Revenue;

//         // Miscellaneous data
//         string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
//         string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
//         string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
//         string placement = adInfo.Placement; // The placement this ad's postbacks are tied to

//         var info = adInfo;

//         //adjust tracking
//         // var adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
//         // adRevenue.setRevenue(info.Revenue, "USD");
//         // adRevenue.setAdRevenueNetwork(info.NetworkName);
//         // adRevenue.setAdRevenueUnit(info.AdUnitIdentifier);
//         // adRevenue.setAdRevenuePlacement(info.Placement);
//         // Adjust.trackAdRevenue(adRevenue);
        
//         var data = new ImpressionData();
//         data.AdFormat = adInfo.AdFormat;
//         data.AdUnitIdentifier = adUnitIdentifier;
//         data.CountryCode = countryCode;
//         data.NetworkName = networkName;
//         data.Placement = placement;
//         data.Revenue = revenue;

        
//         AnalyticsRevenueAds.SendEvent(data);
//         //track here
//     }
// }
