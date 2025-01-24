using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] GameObject player;
    [SerializeField] float CameraMovementSpeed;
    [SerializeField] bool cameraActive = true;

    void Start()
    {
        cameraOffset = (gameObject.transform.position - player.transform.position);
    }

    void Update()
    {
        if (cameraActive)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + cameraOffset, Time.deltaTime * CameraMovementSpeed);
        }
    }
    public void SetInactive()
    {
        cameraActive = false;
    }
    public void SetActive()
    {
        cameraActive = true;
    }
}