using UnityEngine;
using TMPro;
using System.Collections;

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fadeInTime = 2f;
    public float holdTime = 6f;
    public float fadeOutTime = 2f;

    private void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        Color c = text.color;

        // Start fully transparent
        c.a = 0f;
        text.color = c;

        // Fade in
        for (float t = 0; t < fadeInTime; t += Time.deltaTime)
        {
            float normalized = t / fadeInTime;
            c.a = normalized;
            text.color = c;
            yield return null;
        }

        // Ensure full alpha
        c.a = 1f;
        text.color = c;

        // Hold fully visible
        yield return new WaitForSeconds(holdTime);

        // Fade out
        for (float t = 0; t < fadeOutTime; t += Time.deltaTime)
        {
            float normalized = t / fadeOutTime;
            c.a = 1f - normalized;
            text.color = c;
            yield return null;
        }

        // Fully transparent at end
        c.a = 0f;
        text.color = c;
    }
}
