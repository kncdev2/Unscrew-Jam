// using System;
// using System.Collections;
// using System.Collections.Generic;
// using DG.Tweening;
// using UnityEngine;
// using UnityEngine.Purchasing;
// using UnityEngine.Purchasing.Extension;

// public class NoAdsButton : MonoBehaviour
// {
//     [SerializeField]
//     private CodelessIAPButton iapButton;
//     [SerializeField]
//     private GameObject restoreButton;

//     public void StartRequestIAP()
//     {
//         // AdsManager.IsPurchasing = true;
//     }
//     public void RemoveAds(Product product)
//     {
//         Debug.Log("Buy success: "+product.definition.id);
//         AdManager.IsRemoveAds = true;
//         AdManager.Instance.HideBanner();
//         Timer.DelayedCall(0.2f,delegate {         
//             gameObject.SetActive(false);
//         });
//         // restoreButton.SetActive(false);
//         PurchaseDone();
//     }
    
// 	public void RemoveAds()
// {
// 	RemoveAds(null);
// }

//     public void RemoveAdsTest()
//     {
// //        if (AdsManager.IsTestPurchase)
// //        {
// //            AdsManager.IsRemoveAds = true;
// //            AdsManager.Instance.HideBanner();
// //            gameObject.SetActive(false);
// //        }
//     }

//     void PurchaseDone()
//     {
//         // Timer.DelayedCall(1f, delegate
//         // {
//         //     AdsManager.IsPurchasing = false;
//         // });
//     }
//     private void OnPurchaseFail(Product arg0, PurchaseFailureDescription arg1)
//     {
//         Debug.Log("Purchase Fail "+arg0.definition.id+"-- because: "+arg1);
//     }
   

//     private void Start()
//     {
        
//         if (AdManager.IsRemoveAds)
//         {
//             gameObject.SetActive(false);
//             // restoreButton.SetActive(false);
//         }
//         else
//         {
            
//             iapButton.onPurchaseFailed.AddListener(OnPurchaseFail);
//             iapButton.onPurchaseComplete.AddListener(RemoveAds);
//         }
//     }

// }
