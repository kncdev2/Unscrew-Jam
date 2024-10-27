using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HG
{
    [RequireComponent(typeof(Toggle))]
    public class EasySetting : MonoBehaviour
    {
        public enum Type
        {
            Sfx,
            Music,
            Vibrate
        }

        public Type type;
        private Toggle toggle;
        [SerializeField]
        private bool reverse;
        [SerializeField]
        private UnityEvent on, off;

        void Start()
        {

            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnChangeValue);
            //        OnChangeValue(toggle);
            switch (type)
            {
                case Type.Sfx:
                    toggle.isOn = reverse ? SoundManager.I.IsMuteSound : !SoundManager.I.IsMuteSound;
                    break;
                case Type.Music:
                    toggle.isOn = reverse ? SoundManager.I.IsMuteMusic : !SoundManager.I.IsMuteMusic;
                    break;
                case Type.Vibrate:
                    toggle.isOn = reverse ? VibrateManager.I.IsMute : !VibrateManager.I.IsMute;
                    break;
            }

            if (toggle.isOn)
            {
                @on?.Invoke();
            }
            else
            {
                off?.Invoke();
            }

        }
        void OnChangeValue(bool change)
        {
            switch (type)
            {
                case Type.Sfx:
                    SoundManager.I.IsMuteSound = reverse ? change : !change;
                    break;
                case Type.Music:
                    SoundManager.I.IsMuteMusic = reverse ? change : !change;
                    break;
                case Type.Vibrate:
                    VibrateManager.I.IsMute = reverse ? change : !change;
                    break;
            }
            if (toggle.isOn)
            {
                @on?.Invoke();
            }
            else
            {
                off?.Invoke();
            }
            Debug.Log("toggle " + type + ": " + toggle.isOn);

        }

    }
}
