using UnityEngine;

public class Particlecolorchanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    EnemyColorChanger enemyColorChanger;
    Renderer particleRender;
    public int localColor;
    void Awake()
    {
        particleRender=GetComponent<Renderer>();
        enemyColorChanger = GetComponent<EnemyColorChanger>();
       
        if (localColor ==0)
        {
            particleRender.material.SetColor("_Color", Color.green);
        }
        if (localColor == 1)
        {
            particleRender.material.SetColor("_Color", Color.red);
        }
        if (localColor == 2)
        {
            particleRender.material.SetColor("_Color", Color.cyan);
        }
        if (localColor == 3)
        {
            particleRender.material.SetColor("_Color", Color.magenta);
        }
        if (localColor == 4)
        {
            particleRender.material.SetColor("_Color", Color.yellow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
