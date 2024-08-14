using System.Collections;
using UnityEngine;

namespace TheGuild.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediately()
        {
            canvasGroup.alpha = 1f;
        }

        public IEnumerator FadeOutRoutine(float fadeTime)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / fadeTime;
                yield return null;
            }
        }

        public IEnumerator FadeInRoutine(float fadeTime)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeTime;
                yield return null;
            }
        }
    }
}
