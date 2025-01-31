using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset; //the difference between the cameras target and player so the player does not have to be in the exact center of the screen
    [SerializeField] GameObject player; //links to the player so the script can track it
    [SerializeField] float CameraMovementSpeed; //how fast the camera goes to the target
    [SerializeField] bool cameraActive = true; //if the camera follows the player or not
    [SerializeField] Animator cameraShake;

    void Start()
    {
        cameraOffset = (gameObject.transform.position - player.transform.position); //sets up the cameraOffset
    }

    void Update()
    {
        if (cameraActive)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + cameraOffset, Time.deltaTime * CameraMovementSpeed); //uses lerp to move the camera at different speeds depending on how far away the target is
        }
    }
    public void SetInactive()  //public void for switching camera on/off
    {
        cameraActive = false;
    }
    public void SetActive()
    {
        cameraActive = true;
    }
    public void triggerShake()
    {
        cameraShake.SetTrigger("Shake");
    }
}