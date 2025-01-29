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
        for (float i = 0; i < transitionDuration; i += Time.deltaTime/transitionDuration)
        {
            Debug.Log(i);
            //slowly lowers the opacity (last number of the color vector) from 1 (Fully black) to 0 (invisible)
            //WaitForEndOfFrame stops it from triggering multiple times in a frame
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
        for (float i = 0; i < transitionDuration; i += Time.deltaTime/transitionDuration)
        {
            Debug.Log(i);
            spriteRenderer.color = new Color(0f, 0f, 0f, i);
            yield return new WaitForEndOfFrame();
        }
    }
}
