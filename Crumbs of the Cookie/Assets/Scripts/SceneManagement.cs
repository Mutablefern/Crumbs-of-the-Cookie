using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] GameObject Transitioner;
    SceneTransition sceneTransitionScript;
    int transitionSeconds;

    private void Start()
    {
        sceneTransitionScript = Transitioner.GetComponent<SceneTransition>();
    }
    public void changescene(string sceneName)
    {
        StartCoroutine(WaitForTransitionCor(sceneName));
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator WaitForTransitionCor(string sceneName)
    {
        transitionSeconds = sceneTransitionScript.transitionDuration;
        sceneTransitionScript.FadeOut();
        yield return new WaitForSeconds(transitionSeconds);
        SceneManager.LoadScene(sceneName);
    }
}
