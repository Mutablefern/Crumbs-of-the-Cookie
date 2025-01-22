using UnityEngine;

public class PointButtonAdd : MonoBehaviour
{
    PointCalculation pointCalculationScript;

    private void Start()
    {
        pointCalculationScript = GetComponent<PointCalculation>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointCalculationScript.GainPoints(100);
        }
    }
}
