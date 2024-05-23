using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesManager
{
    public class ScreenFader : MonoBehaviour
    {
        public float fadeDuration = 2f;
        private Image fadeImage;

        private void Awake()
        {
            fadeImage = GetComponent<Image>();
        }

        public void FadeToBlack()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float alpha = timer / fadeDuration;
                fadeImage.color = new Color(0f, 0f, 0f, alpha);
                yield return null;
            }
        }
    }
}