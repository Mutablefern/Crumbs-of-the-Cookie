using UnityEngine;

public class Particlecolorchanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    ParticleSystem particleRender;
    Renderer particleRenderer2;
    public int localColor;
    ParticleSystem.MainModule startColor; 

    //Used to give the slime particles the same color as the enemy they spawn from
    void Awake()
    {
        
        particleRender = GetComponent<ParticleSystem>();
        particleRenderer2 = GetComponent<Renderer>();
        startColor = particleRender.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(localColor);
        //Changes the particles color to match the enemy they spawn from
        if (localColor == 0)
        {
            startColor.startColor = Color.green;
        }
        if (localColor == 1)
        {
            startColor.startColor = Color.red;
        }
        if (localColor == 2)
        {
            startColor.startColor = Color.cyan;
        }
        if (localColor == 3)
        {
            startColor.startColor = Color.magenta;
        }
        if (localColor == 4)
        {
            startColor.startColor = Color.yellow;
        }
    }
}
