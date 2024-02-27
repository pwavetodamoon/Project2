using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.Pool;

namespace ObjectPool
{
    public class ObjectPoolPrefab<T> where T : MonoBehaviour, IPooled<T>
    {
        private T prefab;
        private readonly int maxPoolSize;
        private readonly Queue<T> queue;
        private readonly Transform parent;
        private readonly int maxPoolSizeDefault = 20;

        public ObjectPoolPrefab(T _prefab, Transform _parent, int _size)
        {

            queue = new Queue<T>();
            prefab = _prefab;
            parent = _parent;
            maxPoolSize = _size;
            if (maxPoolSize <= 0)
            {
                maxPoolSize = maxPoolSizeDefault;
            }
        }

        public T Get()
        {
            if (queue.Count == 0)
            {
                var newT = Create();
                queue.Enqueue(newT);
            }
            var t = queue.Dequeue();
            t.gameObject.SetActive(true);
            return t;
        }

        private T Create()
        {
            var t = Object.Instantiate(prefab);
            t.transform.SetParent(parent, false);
            t.ReleaseCallback = Release;
            t.gameObject.SetActive(false);
            return t;
        }


        private void Release(T t)
        {
            if (queue.Contains(t))
                return;
            if (queue.Count >= maxPoolSize)
            {
                Object.Destroy(t.gameObject);
            }
            t.gameObject.SetActive(false);
            queue.Enqueue(t);
        }
    }
}