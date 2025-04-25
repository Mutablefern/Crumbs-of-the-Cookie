using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.AltGr) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Debug.Log("SLOW DOWN");
            Time.timeScale /= 2;
        }
    }
}
