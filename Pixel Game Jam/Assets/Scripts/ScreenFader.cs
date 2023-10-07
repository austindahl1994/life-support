using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public float fadeWaitTime = 1.0f;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeScreen());
    }

    IEnumerator FadeScreen()
    {
        // Fade to black
        float t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0.0f, 1.0f, t / fadeDuration);
            image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }

        // Wait for a moment
        yield return new WaitForSeconds(fadeWaitTime);

        // Fade back in
        t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, t / fadeDuration);
            image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }
    }
}
