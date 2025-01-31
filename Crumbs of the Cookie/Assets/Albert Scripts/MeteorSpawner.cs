using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public  enum MeteorType
    {
        Ground,
        House
    }

    public MeteorType MeteorTarget;

    [SerializeField] List <GameObject> strikeTarget; //the target for the meteor
    [SerializeField] int meteorOrigin; //where the meteor is spawned (X)
    [SerializeField] int spawnHeight; //where the meteor is spawned (Y)
    [SerializeField] bool containsEnemy; //if the meteor has enemies inside
    [SerializeField] GameObject meteor; //the meteor that is spawned
    bool active = true; //turns on spawner, is turned off after it fires
    Vector3 spawnLocation; //where the meteor is spawned, calculated using meteorOrigin
    Quaternion spawnRotation; //the rotation of the meteor when spawned

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && active) //Spawns meteor when a player reaches the spawner
        {
            active = false; //Deactivates spawner
            switchMeteor();
        }
    }

    public void switchMeteor()
    {
        switch (MeteorTarget)
        {
            case MeteorType.Ground:
                spawnLocation = new Vector3(Random.Range(-10, 10), spawnHeight, 0); //calculates spawnLocation
                GameObject spawnedMeteorGround = Instantiate(meteor, spawnLocation, Quaternion.identity);
                MeteorController MeteorControllerScriptGround = spawnedMeteorGround.GetComponent<MeteorController>();
                MeteorControllerScriptGround.changeTarget(strikeTarget[Random.Range(2, strikeTarget.Count)].transform);
                MeteorControllerScriptGround.MeteorSpawner = gameObject;
                break;

            case MeteorType.House:
                spawnLocation = new Vector3(meteorOrigin, spawnHeight, 0);
                GameObject spawnedMeteorHouse = Instantiate(meteor, spawnLocation, Quaternion.identity);
                MeteorController MeteorControllerScriptHouse = spawnedMeteorHouse.GetComponent<MeteorController>();
                MeteorControllerScriptHouse.changeTarget(strikeTarget[1].transform);
                break;
        }
    }

}