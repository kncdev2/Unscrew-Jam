using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using DG.Tweening;

internal class DelayedEditorAction
{
    internal double TimeToExecute;
    internal Action Action;
    internal bool ForceEvenIfTargetIsGone;

    public DelayedEditorAction(double timeToExecute, Action action, bool forceEvenIfTargetIsGone = false)
    {
        TimeToExecute = timeToExecute;
        Action = action;
        ForceEvenIfTargetIsGone = forceEvenIfTargetIsGone;
    }
}

public static class Timer
{

    private static TimerComponent _timerComponent;
    private static TimerComponent timerComponent
    {
        get
        {
            if (_timerComponent == null)
            {
                _timerComponent = GameObject.FindObjectOfType<TimerComponent>();

                if (_timerComponent == null && !IsQuitting)
                {
                    var timerGO = new GameObject("Timer");
                    _timerComponent = timerGO.AddComponent<TimerComponent>();

                    GameObject.DontDestroyOnLoad(timerGO);
                }
            }

            return _timerComponent;
        }
    }

    public static bool IsFirstFrame
    {
        get { return Time.frameCount <= 1; }
    }

    private static bool IsQuitting { get; set; }


    public static void DiscardAllTimer()
    {
        DOTween.KillAll();
        timerComponent.delayedActions.Clear();
    }

    [RuntimeInitializeOnLoadMethod()]
    public static void OnLoad()
    {
        Application.quitting += () => IsQuitting = true;
    }

#if UNITY_EDITOR
    static List<DelayedEditorAction> delayedEditorActions = new List<DelayedEditorAction>();

    static Timer()
    {
        UnityEditor.EditorApplication.update += EditorUpdate;
    }
#endif

    public static WaitForSecondsRealtime GetWaitForSecondsRealtimeInstruction(float seconds)
    {
        return new WaitForSecondsRealtime(seconds);
    }

    public static WaitForSeconds GetWaitForSecondsInstruction(float seconds)
    {
        return new WaitForSeconds(seconds);
    }

    static void EditorUpdate()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) return;

        var actionsToExecute = delayedEditorActions.Where(dea => UnityEditor.EditorApplication.timeSinceStartup >= dea.TimeToExecute).ToList();

        if (actionsToExecute.Count == 0) return;

        foreach (var actionToExecute in actionsToExecute)
        {
            try
            {
                actionToExecute.Action.Invoke();
            }
            finally
            {
                delayedEditorActions.Remove(actionToExecute);
            }
        }
#endif
    }

    public static void DelayedCall(float delay, Action action, bool forceEvenIfObjectIsInactive = true)
    {
        if (Application.isPlaying)
        {
            if (timerComponent != null) timerComponent.DelayedCall(delay, action, forceEvenIfObjectIsInactive);
        }
#if UNITY_EDITOR
        else
        {
            delayedEditorActions.Add(new DelayedEditorAction(UnityEditor.EditorApplication.timeSinceStartup + delay, action, forceEvenIfObjectIsInactive));
        }
#endif
    }

    public static void AtEndOfFrame(Action action, bool forceEvenIfObjectIsInactive = false)
    {
        DelayedCall(0, action, forceEvenIfObjectIsInactive);
    }
}