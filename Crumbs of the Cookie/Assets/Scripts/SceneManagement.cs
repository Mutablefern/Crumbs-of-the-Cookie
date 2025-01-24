using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
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
