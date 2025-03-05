using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] GameObject Transitioner;
    SceneTransition sceneTransitionScript;
    int transitionSeconds;
    public static Scene prevScene;

    private void Start()
    {
        if (Transitioner != null)
        {
            sceneTransitionScript = Transitioner.GetComponent<SceneTransition>();
        }
    }
    public void changescene(string sceneName)
    {
        prevScene = SceneManager.GetActiveScene();
        StartCoroutine(WaitForTransitionCor(sceneName));
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator WaitForTransitionCor(string sceneName)
    {
        if (sceneTransitionScript != null)
        {
            transitionSeconds = sceneTransitionScript.transitionDuration;
            sceneTransitionScript.FadeOut();
        }
        yield return new WaitForSeconds(transitionSeconds);
        SceneManager.LoadScene(sceneName);
    }

    public void GoToPrevScene()
    {
        changescene(prevScene.name);
    }
}
