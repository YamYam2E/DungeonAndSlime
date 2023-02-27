using System;
using UnityEngine;

namespace Actor
{
    public class Enemy : ActorBase
    {
        [SerializeField] private SpriteRenderer modelSpriteRenderer;
        [SerializeField] private Texture2D texture;
        //
        public void Awake()
        {
            var sprites = Resources.LoadAll<Sprite>("Enemies/" + texture.name);
            
            foreach (var sprite in sprites)
            {
                Debug.Log(sprite.name);
            }
        }
    }
}