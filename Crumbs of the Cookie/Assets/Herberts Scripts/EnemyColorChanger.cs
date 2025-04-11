using UnityEngine;

public class EnemyColorChanger : MonoBehaviour
{
    public int colorValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Renderer enemy_Renderer;
    Particlecolorchanger particlecolorchanger;

    void Start()
    {
        enemy_Renderer = GetComponentInChildren<Renderer>();

        colorValue = Random.Range(0, 4);


    }

    public void ColorChanger(int color)
    {


        if (colorValue == 0)
        {
            enemy_Renderer.material.SetColor("_Color", Color.green);
        }
        if (colorValue == 1)
        {
            enemy_Renderer.material.SetColor("_Color", Color.red);
        }
        if (colorValue == 2)
        {
            enemy_Renderer.material.SetColor("_Color", Color.cyan);
        }
        if (colorValue == 3)
        {
            enemy_Renderer.material.SetColor("_Color", Color.magenta);
        }
        if (colorValue == 4)
        {
            enemy_Renderer.material.SetColor("_Color", Color.yellow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        particlecolorchanger = FindAnyObjectByType<Particlecolorchanger>();
        if (particlecolorchanger != null)
        {
            particlecolorchanger.localColor = colorValue;
        }
        ColorChanger(colorValue);

    }
}
