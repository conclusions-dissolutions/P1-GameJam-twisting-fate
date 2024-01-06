using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public float fadeSpeed;
    public bool fadeDirection = false;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    IEnumerator FadeTo(float target)
    {
        float current = canvasGroup.alpha;
        for (float t = 0.0f; t < 0.95f; t += Time.deltaTime / fadeSpeed)
        {
            canvasGroup.alpha = Mathf.MoveTowards(current, target, t);
            yield return null;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if ((fadeDirection && canvasGroup.alpha == 1) || (!fadeDirection && canvasGroup.alpha == 0))
        {
            StopAllCoroutines();
            return;
        }

        if (fadeDirection)
            StartCoroutine(FadeTo(1.0f));
        else
            StartCoroutine(FadeTo(0.0f));
    }
}
