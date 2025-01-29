using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public int transitionDuration;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeInCor());
    }

    IEnumerator FadeInCor()
    {
        for (float i = 0; i < transitionDuration; i += Time.deltaTime)
        {
            Debug.Log(i);
            spriteRenderer.color = new Color(0f, 0f, 0f, 1-i);
            yield return new WaitForEndOfFrame();
        }
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutCor());
    }

    IEnumerator FadeOutCor()
    {
        for (float i = 0; i < transitionDuration; i += Time.deltaTime)
        {
            Debug.Log(i);
            spriteRenderer.color = new Color(0f, 0f, 0f, i);
            yield return new WaitForEndOfFrame();
        }
    }
}
