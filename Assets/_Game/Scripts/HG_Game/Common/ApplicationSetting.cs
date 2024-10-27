using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Application Setting")]
public class ApplicationSetting : ScriptableObject
{
    private static ApplicationSetting _instance;

    public static ApplicationSetting Instance
    {
        set => _instance = value;
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<ApplicationSetting>("Application Setting");
            }

            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        if (_instance == null)
        {
            _instance = Resources.Load<ApplicationSetting>("Application Setting");
        }
    }

    public bool isNoAds;
    public bool isFullCoin;
    public bool isAdsTest;
    public bool isGizmos;
    public bool isDebug;


}
