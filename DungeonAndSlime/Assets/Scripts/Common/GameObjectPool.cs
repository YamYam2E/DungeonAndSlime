using UnityEngine;
using UnityEngine.Pool;

namespace Common
{
    public abstract class GameObjectPool<T> where T : MonoBehaviour, IPoolObject
    {
        private ObjectPool<T> pool;
        
        protected GameObject parentObject;
        
        protected GameObjectPool()
        {
            pool = new ObjectPool<T>(OnCreate, OnGet, OnReturn, OnDestroy, true, 30, 30);
            parentObject = new GameObject($"PoolingObjects_{typeof(T)}");
        }

        public T Get() => pool.Get();
        public void Return(T target) => pool.Release(target);

        protected abstract T OnCreate();
        protected abstract void OnGet(T target);
        protected abstract void OnReturn(T target);
        protected abstract void OnDestroy(T target);
        public abstract void ReturnAll();
    }
}