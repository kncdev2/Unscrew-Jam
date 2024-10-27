using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TimerComponent : MonoBehaviour
{
    public List<DelayedAction> delayedActions = new List<DelayedAction>();

    public void DelayedCall(float delay, Action action, bool forceEvenIfTargetIsInactive)
    {
        this.enabled = true;

        delayedActions.Add(new DelayedAction { timeToExecute = Time.unscaledTime + delay, action = action, forceEvenIfTargetIsInactive = forceEvenIfTargetIsInactive });
    }
    
    private void Start()
    {
        
        SceneManager.sceneUnloaded += delegate(Scene arg0)
        {
            for (int i = 0; i < delayedActions.Count; i++)
            {
                if (delayedActions[i].forceEvenIfTargetIsInactive)
                {
                    delayedActions.RemoveAt(i);
                }
            }
        };
    }

    private void Update()
    {
        List<DelayedAction> actionsToExecute = null;
        foreach (var action in delayedActions)
        {
            if (Time.unscaledTime >= action.timeToExecute)
            {
                if (actionsToExecute == null) actionsToExecute = new List<DelayedAction>();
                actionsToExecute.Add(action);
            }
        }

        if (actionsToExecute == null || actionsToExecute.Count == 0) return;

        foreach (var action in actionsToExecute)
        {
            try
            {
                action.action.Invoke();
            }
            finally
            {
                delayedActions.Remove(action);
            }
            
        }

        // stop calling update if we have nothing scheduled (DelayedCall will re-enable this)
        if (delayedActions.Count == 0) this.enabled = false;
    }
}

public class DelayedAction
{
    public float timeToExecute;
    public Action action;
    public bool forceEvenIfTargetIsInactive;
}