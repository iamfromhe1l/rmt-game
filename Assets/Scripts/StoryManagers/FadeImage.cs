using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesManager
{
    public class ScreenFader : MonoBehaviour
    {
        public float fadeDuration = 1.5f;
        private Image fadeImage;

        private void Awake()
        {
            fadeImage = GetComponentInChildren<Image>();
            fadeImage.gameObject.SetActive(false);
        }

        public void FadeToBlack()
        {
            Debug.Log("Start Fade Exec Inside");
            fadeImage.gameObject.SetActive(true);
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