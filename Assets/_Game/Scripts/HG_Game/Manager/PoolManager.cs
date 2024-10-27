using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace HG
{
    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<string, ObjectPool<GameObject>> pools = new();

        public ObjectPool<GameObject> CreatePool(string poolName, Func<GameObject> onCreate, int initObjects = 10, int maxObjects = 10000, bool checkOnCollect = true)
        {
            if (!pools.ContainsKey(poolName))
            {
                pools[poolName] = new ObjectPool<GameObject>(onCreate, GetSetup, ReleaseSetup, DestroySetup, checkOnCollect, initObjects, maxObjects);
            }

            return pools[poolName];
        }
        public ObjectPool<GameObject> CreatePool(string poolName, GameObjectPool pool)
        {
            if (!pools.ContainsKey(poolName))
            {
                pools[poolName] = pool.GetPool();
            }

            return pools[poolName];
        }

        public void ReleaseToPool(string poolName, GameObject obj)
        {
            if (pools.ContainsKey(poolName))
            {
                pools[poolName].Release(obj);
            }
        }

        private void GetSetup(GameObject obj) => obj.gameObject.SetActive(true);
        private void ReleaseSetup(GameObject obj) => obj.gameObject.SetActive(false);
        private void DestroySetup(GameObject obj) => Destroy(obj);
    }
}

