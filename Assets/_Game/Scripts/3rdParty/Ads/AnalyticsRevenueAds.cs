// using Firebase.Analytics;
// using Firebase.Extensions;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using UnityEngine;


// public class AnalyticsRevenueAds
// {
//     public static string AppsflyerID;
//     public static string FirebaseID;


//     public static void SendEvent(ImpressionData data)
//     {
//         SendEventRealtime(data);


//     }

//     private static void SendEventRealtime(ImpressionData data)
//     {
//         Firebase.Analytics.Parameter[] AdParameters = {
//              new Firebase.Analytics.Parameter("ad_platform", "applovin"),
//              new Firebase.Analytics.Parameter("ad_source", data.NetworkName),
//              new Firebase.Analytics.Parameter("ad_unit_name", data.AdUnitIdentifier),
//              new Firebase.Analytics.Parameter("currency","USD"),
//              new Firebase.Analytics.Parameter("value",data.Revenue),
//              new Firebase.Analytics.Parameter("placement",data.Placement),
//              new Firebase.Analytics.Parameter("country_code",data.CountryCode),
//              new Firebase.Analytics.Parameter("ad_format",data.AdFormat),
//         };

//         Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", AdParameters);
//     }


// }

// public class ImpressionData
// {
//     public string CountryCode;
//     public string NetworkName;
//     public string AdUnitIdentifier;
//     public string Placement;
//     public double Revenue;
//     public string AdFormat;

// }

// public enum AdFormat
// {
//     interstitial,
//     video_rewarded,
//     banner
// }
