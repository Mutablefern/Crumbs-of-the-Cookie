using UnityEngine;

public class Parallaxscript : MonoBehaviour
{
    private float startPos, startPosY, length;
    public GameObject cam;
    public float parallaxEffect; // The speed of the background relative to the camera
    public float parallaxEffectY;
    private float distanceY;

    void Start()
    {
        startPos = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        //calculate distance background move based on cam movement
        float distance = cam.transform.position.x * parallaxEffect; 
        float movement = cam.transform.position.x * (1-parallaxEffect);

        distanceY = cam.transform.position.y * parallaxEffectY;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x,startPosY + distanceY, transform.position.z);

        if(movement > startPos + length)
        {
            startPos += distance;   
        }
        else if(movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
