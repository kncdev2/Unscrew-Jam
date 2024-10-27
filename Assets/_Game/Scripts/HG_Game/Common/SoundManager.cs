using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace HG
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField]
        private AudioSource speaker;
        private AudioSource music;
        private SoundTable _soundTable;

        public static event Action<bool> OnMuteSound;

        public SoundTable SoundTable
        {
            get
            {
                if (_soundTable == null)
                {
                    _soundTable = Resources.Load<SoundTable>("Sound Table");
                }

                return _soundTable;
            }
            set => _soundTable = value;
        }

        public bool IsMuteSound
        {
            get => PlayerPrefs.GetInt("sound", 0) == 1;
            set
            {
                speaker.mute = value;
                PlayerPrefs.SetInt("sound", value ? 1 : 0);
                OnMuteSound?.Invoke(value);
            }
        }
        public bool IsMuteMusic
        {
            get => PlayerPrefs.GetInt("music", 0) == 1;
            set
            {
                music.mute = value;
                PlayerPrefs.SetInt("music", value ? 1 : 0);
            }
        }

        public void Init()
        {
            SoundTable = Resources.Load<SoundTable>("Sound Table");
            Awake();
        }
        private void Awake()
        {
            if (speaker == null)
            {
                speaker = gameObject.AddComponent<AudioSource>();
            }

            if (music == null)
            {
                music = gameObject.AddComponent<AudioSource>();
            }

            speaker.mute = IsMuteSound;
            music.mute = IsMuteMusic;
            music.volume = 1f;
            music.loop = true;
            PlayMusic(SoundTable.bg, 0);
        }

        public void ForceStopMusic()
        {
            music.Stop();
        }

        public void StopMusic()
        {
            float defaultVolume = music.volume;
            DOTween.To(f => music.volume = f, defaultVolume, 0, 0.5f).onComplete += delegate
            {
                music.Stop();
                music.volume = defaultVolume;
            };
        }
        public void StopSound()
        {
            float defaultVolume = speaker.volume;
            DOTween.To(f => speaker.volume = f, defaultVolume, 0, 0.5f).onComplete += delegate
            {
                speaker.Stop();
                speaker.volume = defaultVolume;
            };
        }
        public void StopMusic(Action complete)
        {
            float defaultVolume = music.volume;
            DOTween.To(f => music.volume = f, defaultVolume, 0, 0.5f).onComplete += delegate
            {
                music.Stop();
                music.volume = defaultVolume;
                complete.Invoke();
            };
        }

        public void PauseMusic()
        {
            music.Pause();
        }

        public void ResumeMusic()
        {
            music.UnPause();
        }

        public void PlayMusic()
        {
            PlayMusic(SoundTable.bg, 0);

        }

        public void PlayAgainMusic(float start)
        {
            music.time = start;
            music.Play();
        }

        public float MusicTime => music.time;
        public void SetTime(float time)
        {
            music.time = time;
            Debug.Log("set time ???");
        }

        public float TotalMusicTime => music.clip.length;

        public void SetVolume(float value)
        {
            speaker.volume = value;
        }
        public void PlayMusic(AudioClip audioClip, float start, bool isLoop = true)
        {
            if (music.isPlaying)
            {
                StopMusic(delegate
                {
                    music.clip = audioClip;
                    music.Play();
                    music.time = start;
                });
                return;
            }
            music.clip = audioClip;
            music.loop = isLoop;
            Debug.Log("play music: " + audioClip);
            music.Play();
            music.time = start;
        }
        public void PlayOneShot(AudioClip audioClip)
        {
            speaker.PlayOneShot(audioClip);
        }
    }
}
