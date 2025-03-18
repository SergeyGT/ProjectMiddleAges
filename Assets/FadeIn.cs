using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public float fadeDuration = 2f;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeInEffect());
    }

    private System.Collections.IEnumerator FadeInEffect()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = 1 - (time / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
        gameObject.SetActive(false); 
    }
}
