using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    //Used to summon all of the particle effects
    [SerializeField] List<ParticleSystem> particle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

   
    public void Particels(int ParticleType, Vector3 particlePosition)
    {
        //Summons the particle effects
        Instantiate(particle[ParticleType], particlePosition, Quaternion.identity);
    }

}
