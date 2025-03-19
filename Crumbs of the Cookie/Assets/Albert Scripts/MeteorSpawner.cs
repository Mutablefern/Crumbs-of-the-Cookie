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

    [SerializeField] List <GameObject> strikeTarget; //the target(s) for the meteor
    [SerializeField] int meteorSpread; //How much x - deviation the meteor can have (Only used for randomly targeting meteors); 
    [SerializeField] int spawnHeight; //what height relative to this the meteor is spawned (Y)
    [SerializeField] bool containsEnemy; //if the meteor has enemies inside
    [SerializeField] GameObject meteor; //the meteor prefab that is spawned
    bool active = true; //turns on spawner, is turned off after it launches the first meteor
    Vector3 spawnLocation; //where the meteor is spawned

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && active) //Spawns meteor when a player triggers the spawner
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
                spawnLocation = transform.position + new Vector3(Random.Range(-meteorSpread, meteorSpread), spawnHeight, 0); //calculates spawnLocation relative to this.
                GameObject spawnedMeteorGround = Instantiate(meteor, spawnLocation, Quaternion.identity);
                MeteorController MeteorControllerScriptGround = spawnedMeteorGround.GetComponent<MeteorController>(); //Gets the script from the meteor
                MeteorControllerScriptGround.changeTarget(strikeTarget[Random.Range(0, strikeTarget.Count)].transform); //Randomly chooses a target on the ground
                MeteorControllerScriptGround.MeteorSpawner = gameObject; // makes the meteor able to spawn another one it crashes down
                break;

            case MeteorType.House:
                spawnLocation = transform.position + new Vector3(transform.position.x, spawnHeight, 0); //sets the meteor spawn position to spawnHeight above this object
                GameObject spawnedMeteorHouse = Instantiate(meteor, spawnLocation, Quaternion.identity);
                MeteorController MeteorControllerScriptHouse = spawnedMeteorHouse.GetComponent<MeteorController>();
                MeteorControllerScriptHouse.changeTarget(strikeTarget[1].transform); //Sets the target to a specific house
                break;
        }
    }

}