using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeParticles : MonoBehaviour
{
    [SerializeField] List<ParticleSystem>  particle;
    [SerializeField] Transform test;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Particels(1, test.position );
        }
       
       
    }
    public void Particels(int ParticleType,Vector3 particlePosition )
    {
        if (ParticleType == 1)
        {
            Instantiate(particle[0], particlePosition, Quaternion.identity);
            
        }
    }
    
}
