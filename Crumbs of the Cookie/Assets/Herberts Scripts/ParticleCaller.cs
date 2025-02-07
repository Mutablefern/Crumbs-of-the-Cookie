using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleCaller : MonoBehaviour
{
    //Spimle script used to summon different particle effects
    [SerializeField] int particleToCall; // Required to select which particle system is used
    ParticlesManager particlesManager;
    void Start()
    {
        particlesManager = GameObject.Find("Particle Manager").GetComponent<ParticlesManager>();
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            particlesManager.Particels(particleToCall, transform.position);
        }
    }
}
