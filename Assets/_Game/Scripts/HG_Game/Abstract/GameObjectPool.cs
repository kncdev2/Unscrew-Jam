using System;
using UnityEngine;
using UnityEngine.Pool;
namespace HG
{
    public abstract class GameObjectPool : MonoBehaviour
    {
        private GameObject prefab;
        private ObjectPool<GameObject> _pool;
        private ObjectPool<GameObject> Pool
        {
            get
            {
                if (_pool == null)
                {
                    throw new InvalidOperationException("Pooler: no pool found, please init before use");
                }
                return _pool;
            }

            set => _pool = value;
        }

        protected void InitPool(GameObject objectPrefab, int initObjects = 10, int maxObjects = 20, bool checkOnCollect = false)
        {
            prefab = objectPrefab;
            Pool = new ObjectPool<GameObject>(CreateSetup, GetSetup, ReleaseSetup, DestroySetup, checkOnCollect, initObjects, maxObjects);
        }

        protected virtual GameObject CreateSetup() => Instantiate(prefab);
        protected virtual void GetSetup(GameObject obj) => obj.gameObject.SetActive(true);
        protected virtual void ReleaseSetup(GameObject obj) => obj.gameObject.SetActive(false);
        protected virtual void DestroySetup(GameObject obj) => Destroy(obj);

        public GameObject Spawn() => Pool.Get();
        public void Release(GameObject obj) => Pool.Release(obj);
        public ObjectPool<GameObject> GetPool() => Pool;

    }
}
