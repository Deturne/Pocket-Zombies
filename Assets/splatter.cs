using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class splatter : MonoBehaviour
{
    [SerializeField] private Image damageImage;      // Reference to the Image component
    [SerializeField] private float fadeDuration = 1f; // Time it takes to fade out
    [SerializeField] private Color damageColor = new Color(1f, 0f, 0f, 0.5f); // Damage color with transparency

    private Coroutine fadeCoroutine;

    void Start()
    {
        if (damageImage != null)
        {
            damageImage.color = new Color(damageColor.r, damageColor.g, damageColor.b, 0f); // Fully transparent
        }
    }

    public void ShowDamageEffect()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine); // Stop any existing fade-out coroutine
        }
        fadeCoroutine = StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, damageColor.a, elapsedTime / (fadeDuration / 2));
            damageImage.color = new Color(damageColor.r, damageColor.g, damageColor.b, alpha);
            yield return null;
        }

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(damageColor.a, 0f, elapsedTime / (fadeDuration / 2));
            damageImage.color = new Color(damageColor.r, damageColor.g, damageColor.b, alpha);
            yield return null;
        }

        damageImage.color = new Color(damageColor.r, damageColor.g, damageColor.b, 0f); // Ensure fully transparent at the end
        fadeCoroutine = null;
    }

}
