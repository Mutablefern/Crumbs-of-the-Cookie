using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // everything is static, so it can be called from anywhere, as long as SceneManagement is in a builded scene.
    public static void changescene(string sceneName)
    {
        //to change scene, use "Scenemanagement.changescene([the scene you want to change to])"
        SceneManager.LoadScene(sceneName);
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
