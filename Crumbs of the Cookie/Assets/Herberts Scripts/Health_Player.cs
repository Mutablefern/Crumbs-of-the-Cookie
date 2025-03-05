using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour
{
    public static int playerHealth = 1 ;
    //[SerializeField] int playerHealth;
    [SerializeField] List<GameObject> armorIcon;
    [SerializeField] int healthIcon;
    SceneManagement sceneManagement;
    ParticlesManager particlesManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneManagement = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        //particlesManager = GameObject.Find("Particle Manager").GetComponent<ParticlesManager>();
        SetHealth();
    }

    // Update is called once per frame
    void Update()

    {
        if (playerHealth == 0)
        {
            Die();
        }
    }
    public void Health(int dmg)
    {
        //particlesManager.Particels(2, transform.position);
        //When called with a negative int, the players health is lowered. When called with a positive int player health is increased
        if (dmg > 0  && playerHealth >= 0)
        {
            playerHealth -= 1;
            healthIcon -= 1;
            armorIcon[healthIcon].SetActive(false);
        }

        if (dmg < 0)
        {
            playerHealth += 1;
            armorIcon[healthIcon].SetActive(true);
            healthIcon += 1;

        }
        // if it is called with an number bigger than the amount of health the player has, the player instantly dies
        if (dmg >= playerHealth)
        {
            playerHealth = 0;
        }
    }
    void SetHealth()
    {
        //Used to let the UI know how much health the player has  when the player swaps scene
        if (playerHealth <= 0)
        {
            playerHealth = 1;
        }
        healthIcon = playerHealth; 
        for (int i = healthIcon; i > 1; i--)
        {
            healthIcon -= 1;
            armorIcon[healthIcon].SetActive(true);
        }
        healthIcon = playerHealth;
    }
    void Die()
    {
        sceneManagement.ChangeScene("DeathScene");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("OW");
            Health(1);
        }
    }

}
