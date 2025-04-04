using UnityEngine;

public class EnemyColorChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Renderer enemy_Renderer;

    void Start()
    {
        enemy_Renderer = GetComponentInChildren<Renderer>();
        ColorChanger();
       
    }

    void ColorChanger()
    {
         
        int colorValue = Random.Range(0,5);
        //Green and cyan are basiacally the same, Red and magenta are basically the same.
        Debug.Log(colorValue);
        if (colorValue == 0)
        {
            enemy_Renderer.material.SetColor("_Color", Color.green);
        }
        if (colorValue == 1)
        {
            enemy_Renderer.material.SetColor("_Color", Color.cyan);
        }
        if (colorValue == 2)
        {
            enemy_Renderer.material.SetColor("_Color", Color.red);
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            ColorChanger();
        }
    }
}
