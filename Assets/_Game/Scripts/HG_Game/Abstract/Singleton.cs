using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HG
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance = null;

        public static bool IsAwake { get { return (_instance != null); } }


        public static T I
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {

                        string goName = typeof(T).ToString();

                        GameObject gameobject = GameObject.Find(goName);
                        if (gameobject == null)
                        {
                            gameobject = new GameObject();
                            gameobject.name = goName;
                        }

                        _instance = gameobject.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// for garbage collection
        /// </summary>
        public virtual void OnApplicationQuit()
        {
            // release reference on exit
            _instance = null;
        }

        /// <summary>
        /// parent this to another gameobject by string
        /// call from Awake if you so desire
        /// </summary>
        protected void SetParent(string name)
        {
            if (name != null)
            {
                GameObject parentObj = GameObject.Find(name);
                if (parentObj == null)
                {
                    parentObj = new GameObject();
                    parentObj.name = name;
                }
                this.transform.parent = parentObj.transform;
            }


        }
    }
}