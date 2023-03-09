using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Actor
{
    public class Enemy : ActorBase
    {
        [SerializeField] private SpriteRenderer modelSpriteRenderer;
        [SerializeField] private Animator animator;

        public void Awake()
        {
            animator.Play("Enemy (2)_run");
        }
    }
}