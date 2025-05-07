using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (playerHealth <= 0)
        {
            audioManager.playSFX(audioManager.playerdie);

            Die();
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
    public void Die()
    {
        StartCoroutine("Die_Cor");
    }
    IEnumerator Die_Cor()
    {
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<MovementScript>());
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        Destroy(GetComponent<AttackScript>());
        Destroy(GetComponentInChildren<Animator>());
        yield return new WaitForSeconds(0.6f);
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
