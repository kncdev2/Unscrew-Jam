// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Firebase.Analytics;
// using Firebase.Extensions;
// using Firebase.RemoteConfig;
// using Newtonsoft.Json;
// using UnityEngine;

// public class FirebaseInit : MonoBehaviour
// {
//     public static bool IsInit = false;
//     public const int Version = 1;

//     [RuntimeInitializeOnLoadMethod]
//     public static void Init()
//     {
//         Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
//             var dependencyStatus = task.Result;
//             if (dependencyStatus == Firebase.DependencyStatus.Available) {
//                 // Create and hold a reference to your FirebaseApp,
//                 // where app is a Firebase.FirebaseApp property of your application class.

//                InitFirebaseRemote();
//                FirebaseAnalytics.LogEvent("open_app");

//                 // Set a flag here to indicate whether Firebase is ready to use by your app.
//             } else {
//                 UnityEngine.Debug.LogError(System.String.Format(
//                     "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//                 // Firebase Unity SDK is not safe to use here.
//             }
//         });
//     }


//     public static void InitFirebaseRemote()
//     {
//         Dictionary<string, object> defaults =
//             new Dictionary<string, object>();
                
//         defaults.Add("AdConfig_"+Version,"");

//         FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
//             .ContinueWith(tasks => { FetchDataAsync(); });
//     }
    
//     public static Task FetchDataAsync() {
//         Debug.Log("Fetching data...");
//         System.Threading.Tasks.Task fetchTask =
//             Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
//                 TimeSpan.Zero);
//         return fetchTask.ContinueWithOnMainThread(FetchComplete);
//     }

//     private static void FetchComplete(Task obj)
//     {
//         FirebaseRemoteConfig.DefaultInstance.ActivateAsync().ContinueWith(task =>
//         {
//             Fetch();
//             Debug.Log("fetch success");
//             IsInit = true;
//         });
//     }

//     public static void Fetch()
//     {

//         var adConfig = FirebaseRemoteConfig.DefaultInstance.GetValue("AdConfig_"+Version).StringValue;
        
//         if (string.IsNullOrEmpty(adConfig))
//         {
//             AdManager.config = new AdManager.AdConfig();
//             Debug.Log(JsonConvert.SerializeObject(AdManager.config));
//         }
//         else
//         {
//             try
//             {
//                 AdManager.config = JsonConvert.DeserializeObject<AdManager.AdConfig>(adConfig);
//             }
//             catch (Exception e)
//             {
//                 Debug.Log(e);
//                 AdManager.config = new AdManager.AdConfig();
//             }
//         }     
//         // MenuPopup.OnlyPrank = FirebaseRemoteConfig.DefaultInstance.GetValue("OnlyPrank").BooleanValue;
//         // GameManager.TimeLoading = (float)FirebaseRemoteConfig.DefaultInstance.GetValue("TimeLoading").DoubleValue - 2;
//         //
//         // ShowInterWinLose = FirebaseRemoteConfig.DefaultInstance.GetValue("ShowInterWinLose").BooleanValue;
//         // NameShop.IsSelectGirl = FirebaseRemoteConfig.DefaultInstance.GetValue("IsSelectGirl").BooleanValue;
//         //
//         // var dataConfigTime = FirebaseRemoteConfig.DefaultInstance.GetValue("TimePlayConfig").StringValue;
//         //
//         // if (string.IsNullOrEmpty(dataConfigTime))
//         // {
//         //     LevelManager.ConfigLevel = new LevelManager.LevelSeekConfig();
//         //     Debug.Log(JsonConvert.SerializeObject(LevelManager.ConfigLevel));
//         // }
//         // else
//         // {
//         //     try
//         //     {
//         //         LevelManager.ConfigLevel = JsonConvert.DeserializeObject<LevelManager.LevelSeekConfig>(dataConfigTime);
//         //     }
//         //     catch (Exception e)
//         //     {
//         //         Debug.Log(e);
//         //         LevelManager.ConfigLevel = new LevelManager.LevelSeekConfig();
//         //     }
//         // }        
//         //
//         // var dataRemote = FirebaseRemoteConfig.DefaultInstance.GetValue("ConfigsGame_Ver_"+Version).StringValue;
//         //
//         // if (string.IsNullOrEmpty(dataRemote))
//         // {
//         //     GameManager.DataRemoteConfig = new GameManager.DataRemote();
//         //     Debug.Log(JsonConvert.SerializeObject(GameManager.DataRemoteConfig));
//         // }
//         // else
//         // {
//         //     try
//         //     {
//         //         GameManager.DataRemoteConfig = JsonConvert.DeserializeObject<GameManager.DataRemote>(dataRemote);
//         //     }
//         //     catch (Exception e)
//         //     {
//         //         Debug.Log(e);
//         //         GameManager.DataRemoteConfig = new GameManager.DataRemote();
//         //     }
//         // }
        
//         //ShowBanner = FirebaseRemoteConfig.DefaultInstance.GetValue("ShowBanner").BooleanValue;
//         SyncValueRemote();
//     }

//     public static void SyncValueRemote()
//     {
//         // var remote = GameManager.DataRemoteConfig;
//         // Debug.Log(remote.isShowOpenAds);
//         // Debug.Log(remote.isShowInter);
//         // ControlPopup.CanShowInter = remote.canShowInterWhilePlay;
//         // ControlPopup.time = remote.timeShowInterWhilePlay;
//         // ControlPopup.TimeLevel1 = remote.timeShowInterWhilePlayLevel1;
//         // AdManager.CanShowOpenAds = remote.isShowOpenAds;
//         // AdManager.CanShowInter = remote.isShowInter;
//         // ShowBanner = remote.isShowBanner;
//         // CharacterItemUI.Price = remote.priceCatchMode;
//         // TimeUpPopup.AddTime = remote.addTime;
//         // WinPopup.CoinReward = remote.winCoin;
//         // RewardPopup.Coin = remote.freeCoin;
//         // NewUnlockPopup.IsShowAds = remote.isShowInterInUnlockMode;
//     }
// }
// public static class FirebaseCustomEvent
// {
//     public static void SendEventStart(string level)
//     {
//         FirebaseAnalytics.LogEvent("level_start",new Parameter("level",level));
//     }
//     public static void SendEventWin(string level)
//     {
//         FirebaseAnalytics.LogEvent("level_win",new Parameter("level",level));
//     }
//     public static void SendEventLose(string level)
//     {
//         FirebaseAnalytics.LogEvent("level_lose",new Parameter("level",level));
//     }
// }