using UnityEngine;

public class PointButtonAdd : MonoBehaviour
{
    PointCalculation pointCalculationScript;
    SceneManagement sceneManagementScript;

    private void Start()
    {
        sceneManagementScript = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        pointCalculationScript = GetComponent<PointCalculation>();
        Time.timeScale = 1;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointCalculationScript.GainPoints(100);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Hagha, ONE!");
            sceneManagementScript.changescene("SampleScene");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            sceneManagementScript.changescene("BScene");
        }
    }
}
