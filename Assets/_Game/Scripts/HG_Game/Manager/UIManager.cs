using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HG;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Stack<ScreenItem> _screens = new Stack<ScreenItem>();

    public void Push(string screenName, ScreenData screenData = null)
    {
        ScreenItem screenPrefab = Resources.Load<ScreenItem>(screenName);
        if (screenPrefab != null)
        {
            ScreenItem screenInstance = Instantiate(screenPrefab, transform);
            _screens.Push(screenInstance);

            screenInstance.OnPush(screenData);
        }
        else
        {
            Debug.LogError($"ScreenItem {screenName} not found in Resources.");
        }
    }

    public void Pop()
    {
        if (_screens.Count > 0)
        {
            ScreenItem screenInstance = _screens.Pop();
            screenInstance.OnPop();
            Destroy(screenInstance.gameObject);
        }
        else
        {
            Debug.LogWarning("No screens to pop.");
        }
    }

    public ScreenItem GetCurrentScreen()
    {
        return _screens.Count > 0 ? _screens.Peek() : null;
    }

    public void PopAllScreens()
    {
        while (_screens.Count > 0)
        {
            Pop();
        }
    }
    public ScreenItem GetScreen<T>() where T : ScreenItem
    {
        return _screens.FirstOrDefault(screen => screen is T);
    }

    public ScreenItem GetScreen(ScreenItem screenItem)
    {
        return _screens.FirstOrDefault(screen => screen == screenItem);
    }

    public void PopToScreen<T>() where T : ScreenItem
    {
        while (_screens.Count > 0)
        {
            ScreenItem currentScreen = _screens.Peek();
            if (currentScreen is T)
            {
                return;
            }
            Pop();
        }
    }
}
