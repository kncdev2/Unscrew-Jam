using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HG;
// using MoreMountains.NiceVibrations;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField]
    private bool autoInit = true;
    private void Start()
    {
        if(autoInit)
            GetComponent<Button>().onClick.AddListener(Click);
    }

    public void Click()
    {
        SoundManager.I.SoundTable.click.Play();
        // VibrateManager.Instance.Haptic(HapticTypes.Selection);
    }
}
#if UNITY_EDITOR

public class ClickSoundEditor : EditorWindow
{
    [MenuItem("Tools/Add Sound To Button")]
    static void Init()
    {
        GetWindow<ClickSoundEditor>();
    }
    private void OnGUI()
    {
        Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();
        buttons = buttons.Where(b => !b.GetComponent<ClickSound>()).ToArray();
        for (int i = 0; i < buttons.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(buttons[i].name);
            if (GUILayout.Button("Add"))
            {
                buttons[i].gameObject.AddComponent<ClickSound>();
            }
            GUILayout.EndHorizontal();
        }
    }
}

#endif
