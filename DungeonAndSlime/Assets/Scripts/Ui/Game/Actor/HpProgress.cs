using System;
using UnityEngine;

namespace Ui.Game.Actor
{
    public class HpProgress : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sliderTarget;

        private Vector2 defaultValue;
        
        private void Awake()
        {
            defaultValue = sliderTarget.size;
        }

        public void SetValue(float rate)
        {
            var size = sliderTarget.size;
            size = new Vector2(size.x * rate, size.y);
            sliderTarget.size = size;
        }
    }
}