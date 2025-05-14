using UnityEngine;

public class ParallaxFollow : MonoBehaviour
{
    Transform camTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camTransform=GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, camTransform.position.y, transform.position.z);
    }
}
