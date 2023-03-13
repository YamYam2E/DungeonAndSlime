using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEditor;
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
    
    public class ResourceLoadManager : MonoBehaviourSingleton<ResourceLoadManager>
    {
        public readonly Dictionary<string, Texture2D> preloadedTexture2Ds = new Dictionary<string, Texture2D>();
      
        public readonly Dictionary<string, AnimationClip> preloadedAnimations = new Dictionary<string, AnimationClip>();
      
        public RuntimeAnimatorController preloadedAnimator { get; private set; }

        public bool IsDonePreload;

        protected override void Initialize()
        {
            base.Initialize();

            //Test code for clean caching
            Caching.ClearCache();

            StartCoroutine( InitializeAddressable() );
            // StartCoroutine( LoadAnimatorAsset() );
            // StartCoroutine( LoadAnimationAsset() );
            // StartCoroutine( LoadTextureAsset() );
        }

        private IEnumerator InitializeAddressable()
        {
            var handler = Addressables.InitializeAsync();

            while (!handler.IsDone)
                yield return null;
            
            Debug.Log(handler.Result.LocatorId);
            Debug.Log(string.Join(", ", handler.Result.Keys));
        }

        private IEnumerator LoadAnimatorAsset()
        {
            GameDebug.Log("-> Start to load Animator Asset by addressable");

            var animatorResult
                = Addressables.LoadAssetAsync<RuntimeAnimatorController>(EAddressType.EnemyAnimator.ToString());

            while (!animatorResult.IsDone)
                yield return null;

            preloadedAnimator = animatorResult.Result;
            
            GameDebug.Log($"Completed to load Animator Asset by addressable");
            
            IsDonePreload = true;
        }

        private IEnumerator LoadAnimationAsset()
        {
            GameDebug.Log("-> Start to load Animation Asset by addressable");

            var result = Addressables.LoadAssetsAsync<AnimationClip>(
                EAddressType.EnemyAnimation.ToString(), null);

            while (!result.IsDone)
                yield return null;

            foreach (var data in result.Result)
                preloadedAnimations.Add(data.name, data);
            
            GameDebug.Log($"Completed to load Animation Asset by addressable : total [{preloadedAnimations.Count}]");
        }
        
        private IEnumerator LoadTextureAsset()
        {
            GameDebug.Log("-> Start to load Texture2D Asset by addressable");
            
            var result = Addressables.LoadAssetsAsync<Texture2D>(
                EAddressType.EnemyTexture.ToString(), null);

            while (!result.IsDone)
                yield return null;

            foreach (var data in result.Result)
            {
                preloadedTexture2Ds.Add(data.name, data);
            }
            
            GameDebug.Log($"Completed to load Texture2D Asset by addressable : total [{preloadedTexture2Ds.Count}]");
            
            
        }
    }
}