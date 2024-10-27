#if MOREMOUNTAINS_NICEVIBRATIONS
    using MoreMountains.NiceVibrations;
#endif
using UnityEngine;
namespace HG
{
    public class VibrateManager : Singleton<VibrateManager>
    {
        private bool isMute;

        private bool isStartHaptic;
#if MOREMOUNTAINS_NICEVIBRATIONS
        private HapticTypes types;
#endif

        private float time = 0.1f;
        private float rate;
        public bool IsMute
        {
            get => PlayerPrefs.GetInt("vibrate", 0) == 1;
            set
            {
                isMute = value;
                PlayerPrefs.SetInt("vibrate", value ? 1 : 0);
            }
        }
#if MOREMOUNTAINS_NICEVIBRATIONS

        private void Update()
        {

            if(isMute)
                return;
            
            if (isStartHaptic)
            {
                if (rate <= 0)
                {
                    rate = time;
                    Haptic(types);
#if UNITY_EDITOR
                    Debug.Log("vibrateeeeeee");
#endif
                }
                else
                {
                    rate -= Time.deltaTime;
                }
            }
            
        }
#endif

        public void Init()
        {
        }

        public void Haptic()
        {
        }

        public void VibratePop()
        {
            if (isMute)
            {
                return;
            }
        }
#if MOREMOUNTAINS_NICEVIBRATIONS

        public void Haptic(HapticTypes types)
        {
            if (isMute)
            {
                return;
            }
            MMVibrationManager.Haptic(types);
        }

        public void StartHaptic(HapticTypes types)
        {
            this.types = types;
            isStartHaptic = true;

        }

        public void StopHaptic()
        {
            isStartHaptic = false;

        }
#endif
    }


}