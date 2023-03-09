using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Manager
{
    public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
    {
        protected override void Initialize()
        {
            base.Initialize();
            Addressables.InstantiateAsync("Enemies/Enemy (1)").Completed += instantiate_Completed;
        }

        private void instantiate_Completed(AsyncOperationHandle<GameObject> obj) {
            // Add component to release asset in GameObject OnDestroy event
            //obj.Result.AddComponent(typeof(SelfCleanup));
            Debug.Log(obj.Result);
        }
        
        private void LoadAsset()
        {
        }
    }
}