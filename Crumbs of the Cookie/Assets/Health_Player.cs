using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour
   
{
    [SerializeField] int playerHealth; 
    [SerializeField] List<GameObject> armorIcon;
    [SerializeField] int healthIcon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //healthIcon = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Health(1);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Health(0);
        }
        if(playerHealth == 0)
        {
            Debug.Log("u Dead");
        }
    }
    public void Health( int dmg)
    {
        if(dmg > 0)
        {
            playerHealth -= 1;
            healthIcon -= 1;
            armorIcon[healthIcon].SetActive(false);
        }

        if (dmg < 1)
        {
            playerHealth += 1;
            //healthIcon += 1;
            armorIcon[healthIcon].SetActive(true);  

        }
    }
}
