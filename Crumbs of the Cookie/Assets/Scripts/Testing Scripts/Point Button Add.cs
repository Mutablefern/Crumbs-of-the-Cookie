using UnityEngine;
using UnityEngine.SceneManagement;

public class PointButtonAdd : MonoBehaviour
{
    PointCalculation pointCalculationScript;
    SceneManagement sceneManagementScript;

    private void Start()
    {
        sceneManagementScript = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        pointCalculationScript = GetComponent<PointCalculation>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointCalculationScript.GainPoints(100);
        }

        if (Input.GetMouseButtonDown(1))
        {
            sceneManagementScript.changescene("SampleScene");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            sceneManagementScript.changescene("BScene");
        }
    }
}
