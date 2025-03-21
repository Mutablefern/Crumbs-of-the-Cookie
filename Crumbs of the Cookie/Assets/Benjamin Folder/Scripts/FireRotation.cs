using UnityEngine;

public class FireRotation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
