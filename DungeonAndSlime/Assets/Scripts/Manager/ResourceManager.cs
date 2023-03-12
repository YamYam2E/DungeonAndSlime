using System.Collections;
using System.Collections.Generic;
using Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Manager
{
    public enum EAddressType
    {
        EnemyAnimator,
        EnemyAnimation,
        EnemyTexture,
    }
    
    public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
    {
        private Dictionary<string, Texture2D> preloadedTexture2Ds = new Dictionary<string, Texture2D>();
        private Dictionary<string, Animation> preloadedAnimations = new Dictionary<string, Animation>();
        
        protected override void Initialize()
        {
            base.Initialize();

            StartCoroutine( LoadAnimationAsset() );
            StartCoroutine( LoadTextureAsset() );
        }

        private IEnumerator LoadAnimationAsset()
        {
            GameDebug.Log("Start to load Animation Asset by addressable");
            GameDebug.Log($"Completed to load Animation Asset by addressable : total [{preloadedAnimations.Count}]");
            yield return null;
        }
        
        private IEnumerator LoadTextureAsset()
        {
            GameDebug.Log("Start to load Texture2D Asset by addressable");
            
            var result = Addressables.LoadAssetsAsync<Texture2D>(
                EAddressType.EnemyTexture.ToString(), null);

            while (!result.IsDone)
                yield return null;

            foreach (var data in result.Result)
                preloadedTexture2Ds.Add(data.name, data);
            
            GameDebug.Log($"Completed to load Texture2D Asset by addressable : total [{preloadedTexture2Ds.Count}]");
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