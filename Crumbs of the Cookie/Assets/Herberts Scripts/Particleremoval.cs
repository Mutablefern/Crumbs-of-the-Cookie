using UnityEngine;

public class Particleremoval : MonoBehaviour
{
    [SerializeField] float particletimer;
    [SerializeField] float particletimerorg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
