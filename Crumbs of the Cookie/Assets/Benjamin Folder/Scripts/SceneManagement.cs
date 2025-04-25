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
        Debug.Log(PlayerPrefs.GetString("SavedScene"));
    }

    public void LoadNextScene()
    {
        ChangeScene(null);
    }
    public void ChangeScene(string sceneName)
    {
        if (sceneName != null)
        {
            if ((SceneManager.GetActiveScene().name != ("DeathScene")))
            //Cant go back to death scene, but remembers all other scenes
            {
                PrevScene = SceneManager.GetActiveScene().name;
            }
            StartCoroutine(WaitForTransitionCor(sceneName));
        }
        else 
        {
            Scene sceneLoaded = SceneManager.GetActiveScene();
            SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
        }
    }

    public void Quit()
    {
        SaveScene(true);
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

    public void ContinueGame()
    {
        SceneManager.LoadScene(SaveScene(false));
    }

    string SaveScene(bool toSave)
    {
        if (toSave == true && PrevScene != null)
        {
            PlayerPrefs.SetString("SavedScene", PrevScene);
            Debug.Log(PlayerPrefs.GetString("SavedScene"));
            return "BugScene";
        }    

        else if (toSave == false)
        {
            Debug.Log("Saved String is " + PlayerPrefs.GetString("SavedScene"));
            return PlayerPrefs.GetString("SavedScene");
        }

        else
        {
            Debug.LogError("Unexpected value of bool");
            return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
