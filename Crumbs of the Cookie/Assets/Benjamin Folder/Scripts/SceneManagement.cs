using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] GameObject Transitioner;
    SceneTransition sceneTransitionScript;
    int transitionSeconds;
    public static string PrevScene;

    private void Awake()
    {
        if (Transitioner != null)
        {
            sceneTransitionScript = Transitioner.GetComponent<SceneTransition>();
        }
        Debug.Log("prevScene (Start) " + PrevScene);
    }
    public void ChangeScene(string sceneName)
    {
        if ((SceneManager.GetActiveScene().name != ("DeathScene")))
        {
            PrevScene = SceneManager.GetActiveScene().name;
        }
        Debug.Log("prevscene is (changescene) " + PrevScene);
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
        Debug.Log("prevscene is " + PrevScene);
        ChangeScene(PrevScene);
    }
}
