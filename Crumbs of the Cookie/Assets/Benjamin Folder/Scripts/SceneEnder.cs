using UnityEngine;

public class SceneEnder : MonoBehaviour
{
    SceneManagement SceneManagement_s;

    private void Awake()
    {
        SceneManagement_s = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Only loads the next scene if the player touched it
        {
            SceneManagement_s.LoadNextScene();
        }
    }
}