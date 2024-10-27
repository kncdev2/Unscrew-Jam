using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTimer
{
    Dictionary<string,float> timer = new Dictionary<string, float>();

    public bool CheckTime(string name,float targetTime)
    {
        if (timer.ContainsKey(name))
        {
            timer[name] += Time.deltaTime;
            return timer[name] >= targetTime;

        }
        else
        {
            timer.Add(name,0);
            return false;
        }
    }

    public void ResetTime(string name)
    {
        timer[name] = 0;
    }
}

public interface IDebugLocalTimer
{
    LocalTimer GetLocalTimer();
}

