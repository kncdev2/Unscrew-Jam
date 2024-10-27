// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [Serializable]
// public class InterstitialCondition
// {
//     public int count;
//     public int target;

//     public string Placement { private set; get; }

//     public InterstitialCondition(string placement,int target)
//     {
//         this.target = target;
//         Placement = placement;
//     }

//     public void ShowInter()
//     {
//         count++;
//         if (count % target == 0)
//         {
//              AdManager.Instance.ShowInterstitial(Placement);
//         }
//     }
// }
