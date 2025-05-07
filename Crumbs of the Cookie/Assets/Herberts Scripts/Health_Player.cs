using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour
{
    public static int playerHealth = 1 ;
    [SerializeField] List<GameObject> armorIcon;
    [SerializeField] int healthIcon;
    SceneManagement sceneManagement;
    ParticlesManager particlesManager;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneManagement = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        particlesManager = GameObject.Find("Particle Manager").GetComponent<ParticlesManager>();
        SetHealth();
    }

    // Update is called once per frame
    void Update()

    {
        if (playerHealth == 0)
        {
            audioManager.playSFX(audioManager.playerdie);
            Die();
        }
    }
    public void Health(int dmg)
    {
        particlesManager.Particels(2, transform.position);
        //When called with a negative int, the players health is increased. When called with a positive int player health is decreased
        if (dmg > 0  && playerHealth >= 0)
        {
            playerHealth -= dmg;
            healthIcon -= dmg;
            armorIcon[healthIcon].SetActive(false);
        }

        if (dmg < 0)
        {
            playerHealth += dmg;
            armorIcon[healthIcon].SetActive(true);
            healthIcon += dmg;

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
            armorIcon[healthIcon -1].SetActive(true);
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
            audioManager.playSFX(audioManager.playerhurt);
            Debug.Log("OW");
            Health(1);
        }
    }

}
