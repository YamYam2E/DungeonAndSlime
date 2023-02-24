using UnityEngine;

namespace Common
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    CreateInstance();
                
                return instance;
            }
        }
        
        public static bool HasInstance => instance != null;

        private static void CreateInstance()
        {
            if (instance != null) 
                return;
            
            instance = FindObjectOfType(typeof(T)) as T;
            
            if (instance == null)
                instance = new GameObject($"Instance {typeof(T)}", typeof(T)).GetComponent<T>();
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                Debug.LogError($"Another instance of {GetType().ToString()} is already exist! Destroying self");
                DestroyImmediate(this);
                return;
            }

            Initialize();
        }

        protected virtual void Initialize()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
}