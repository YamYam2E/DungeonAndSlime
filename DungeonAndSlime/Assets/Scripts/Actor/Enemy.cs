using System.Collections;
using Manager;
using UnityEngine;

namespace Actor
{
    public class Enemy : ActorBase
    {
        [SerializeField] private SpriteRenderer modelSpriteRenderer;
        [SerializeField] private Animator animator;

        public void Awake()
        {
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => ResourceLoadManager.Instance.IsDonePreload);
            
            animator. runtimeAnimatorController 
                = ResourceLoadManager.Instance.preloadedAnimator;
            
            animator.Play("Enemy (11)_run");
            
            // texture = ResourceManager.Instance.preloadedTexture2Ds["Enemy (2)"];
            // modelSpriteRenderer.sprite = Sprite.Create(
            //     texture, 
            //     new Rect(0, 0, texture.width, texture.height),
            //     new Vector2(0.5f, 0.5f));
        }
    }
}