using UnityEngine;

public class Particleremoval : MonoBehaviour
{
    //Destroys the particle system after it is done so that they do not take space
    [SerializeField] float particletimer;
    [SerializeField] float particletimerorg;
    
    void Start()
    {
        particletimerorg = particletimer;
    }

    // Update is called once per frame
    void Update()
    {
        
        particletimer -= Time.deltaTime;
        if (particletimer <= 0)
        {
            Destroy(gameObject);
        }
        
    } 
}
