using System.Collections.Generic;
using UnityEngine;

public class BreakingWallTrigger : MonoBehaviour
{
    [SerializeField] GameObject object1;
    [SerializeField] GameObject object2;
    [SerializeField] GameObject object3;
    [SerializeField] GameObject object4;
    BreakingWall triggerScript1;
    BreakingWall triggerScript2;
    BreakingWall triggerScript3;
    BreakingWall triggerScript4;

    public void Start()
    {
        BreakingWall triggerScript1 = object1.GetComponent<BreakingWall>();
        BreakingWall triggerScript2 = object2.GetComponent<BreakingWall>();
        BreakingWall triggerScript3 = object3.GetComponent<BreakingWall>();
        BreakingWall triggerScript4 = object4.GetComponent<BreakingWall>();
    }

    public void OnTriggerEnter2D()
    {
        triggerScript1.BreakWall();
        triggerScript2.BreakWall();
        triggerScript3.BreakWall();
        triggerScript4.BreakWall();
    }

}