using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HG
{
    [CreateAssetMenu(menuName = "Table/Sound Table")]
    public class SoundTable : ScriptableObject
    {

        private static SoundTable _soundTable;

        public static SoundTable Instance
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

        [RuntimeInitializeOnLoadMethod]
        static void OnInit()
        {
            _soundTable = Resources.Load<SoundTable>("Sound Table");
        }

        public AudioClip click, bg, jump, die, win, movePlatform, respawn, coinCollect;
    }
}

