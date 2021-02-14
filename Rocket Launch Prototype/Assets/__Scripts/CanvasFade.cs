using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    public float fadeDuration = 1.5f;

    public void Fade()
    {
        CanvasGroup healthUI = GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(healthUI));
    }

    IEnumerator DoFade(CanvasGroup healthUI)
    {
        float counter = 0f;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            healthUI.alpha = Mathf.Lerp(1f, 0f, counter/fadeDuration);

            yield return null;
        }
    }
}
