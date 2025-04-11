using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] GameObject Transitioner;
    SceneTransition sceneTransitionScript;
    float transitionSeconds;
    public static string PrevScene;
    [Tooltip ("The scenechange duration when the player dies")]
    [SerializeField] float deathDuration;

    private void Awake()
    {
        if (Transitioner != null)
        {
            sceneTransitionScript = Transitioner.GetComponent<SceneTransition>();
        }
    }
    public void ChangeScene(string sceneName)
    {
        if ((SceneManager.GetActiveScene().name != ("DeathScene")))
            //Cant go back to death scene, but remembers all other scenes
        {
            PrevScene = SceneManager.GetActiveScene().name;
        }
        StartCoroutine(WaitForTransitionCor(sceneName));
    }

    public void Quit()
    {
        Application.Quit();
 
    }

    IEnumerator WaitForTransitionCor(string sceneName)
    {
        if (sceneName == "DeathScene")
        {
            //If the scene that is being changed to is the game over scene, this overrides the transitionscript duration
            //Since this is before, the fadeout doesnt trigger
            sceneTransitionScript = null;
            transitionSeconds = deathDuration;
        }
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
        //Goes back to the previous scene
        transitionSeconds = deathDuration;
        ChangeScene(PrevScene);
    }
}
