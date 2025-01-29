using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject strikeTarget; //the target for the meteor
    [SerializeField] int meteorOrigin; //where the meteor is spawned
    [SerializeField] bool containsEnemy; //if the meteor has enemies inside
    [SerializeField] GameObject meteor; //the meteor that is spawned
    bool active = true; //turns on spawner, is turned off after it fires
    Vector3 spawnLocation; //where the meteor is spawned, calculated using meteorOrigin
    Quaternion spawnRotation; //the rotation of the meteor when spawned

    void Start()
    {
        spawnLocation = new Vector3(meteorOrigin, 10, 0); //calculates spawnLocation
        spawnRotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && active) //Spawns meteor when a player reaches the spawner
        {
            active = false; //Deactivates spawner
            GameObject spawnedMeteor = Instantiate(meteor, spawnLocation, spawnRotation);
            spawnedMeteor.GetComponent<MeteorController>().changeTarget(strikeTarget.transform);
        }
    }
}