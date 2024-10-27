using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenItem : MonoBehaviour
{
    public virtual void OnPush(ScreenData screenData) { }
    public virtual void OnPop() { }
}
public class ScreenData
{
    public Dictionary<string, object> data = new Dictionary<string, object>();
    public Dictionary<string, Action> action = new Dictionary<string, Action>();
}