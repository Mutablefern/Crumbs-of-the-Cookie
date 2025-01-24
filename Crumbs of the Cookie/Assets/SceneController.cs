using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    public void LoadNewScene(string HerbertTestScene)
    {
        SceneManager.LoadScene(HerbertTestScene);
    }
    public void LoadStartScene(string Start)
    {
        SceneManager.LoadScene(Start);
    }

    public void LoadGOScene(string GameOver)
    {
        SceneManager.LoadScene(GameOver);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadHowToPlayScene(string HowToPlay)
    {
        SceneManager.LoadScene(HowToPlay);
    }
    public void LoadWinScene(string Win)
    {
        SceneManager.LoadScene(Win);
    }
}
