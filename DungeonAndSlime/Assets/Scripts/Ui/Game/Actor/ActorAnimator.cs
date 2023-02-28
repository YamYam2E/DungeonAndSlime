using System.Collections;
using UnityEngine;

namespace Ui.Game.Actor
{
    public class ActorAnimator : MonoBehaviour
    {
        public Sprite[] sprites;
        public float frameRate = 60.0f;

        private SpriteRenderer spriteRenderer;
        private int index = 0;
        private float timer = 0.0f;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            
            StartCoroutine(AnimateSprites());
        }

        private IEnumerator AnimateSprites()
        {
            while (true)
            {
                if (sprites.Length > 0)
                {
                    spriteRenderer.sprite = sprites[index];
                    index = (index + 1) % sprites.Length;
                }

                var delay = 1.0f / frameRate;
                timer += Time.deltaTime;
                while (timer >= delay)
                {
                    timer -= delay;
                    yield return null;
                }
            }
        }
    }
}