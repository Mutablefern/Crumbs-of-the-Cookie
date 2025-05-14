using System.Collections.Generic;
using UnityEngine;

public class BreakingWallTrigger : MonoBehaviour
{
    [SerializeField] GameObject object1;
    [SerializeField] GameObject object2;
    [SerializeField] GameObject object3;
    [SerializeField] GameObject object4;
    [SerializeField] GameObject object5;
    BreakingWall triggerScript1;
    BreakingWall triggerScript2;
    BreakingWall triggerScript3;
    BreakingWall triggerScript4;
    BreakingWall triggerScript5;
    [SerializeField] bool amountIs5;
    bool triggered = false;

    public void Start()
    {
        triggerScript1 = object1.GetComponent<BreakingWall>();
        triggerScript2 = object2.GetComponent<BreakingWall>();
        triggerScript3 = object3.GetComponent<BreakingWall>();
        triggerScript4 = object4.GetComponent<BreakingWall>();
        if (amountIs5)
        {
            triggerScript5 = object5.GetComponent<BreakingWall>();
        }

    }

    public void OnTriggerEnter2D()
    {
        if (!triggered)
        {
            triggered = true;
            triggerScript1.BreakWall();
            triggerScript2.BreakWall();
            triggerScript3.BreakWall();
            triggerScript4.BreakWall();
            if (amountIs5)
            {
                triggerScript5.BreakWall();
            }
        }
    }

    public void TriggerBreak()
    {
        if (!triggered)
        {
            triggered = true;
            triggerScript1.BreakWall();
            triggerScript2.BreakWall();
            triggerScript3.BreakWall();
            triggerScript4.BreakWall();
            if (amountIs5)
            {
                triggerScript5.BreakWall();
            }
        }
    }

}