using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public enum enemyType
    {
        GummyBear,
        GummyRat,
    };
    [Tooltip("choose what type of enemy this is")]
    [SerializeField] enemyType thisEnemyType;
    [Tooltip("3 or less for rat, more for bear")]
    [SerializeField] int health;
    ParticlesManager particlesManager;

    private void Start()
    {
        //particlesManager = GameObject.Find("Particle Manager").GetComponent<ParticlesManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LightHit"))
        {
            health--;
            //DamageEffect();
        }
        else if (collision.gameObject.CompareTag("HeavyHit"))
        {
            health -= 2;
            //DamageEffect();
        }
        if (health <= 0)
        {
            Debug.Log("Play Enemy death anim");
            Destroy(gameObject);
        }
    }
    /*
    void DamageEffect()
    {
        if (thisEnemyType==enemyType.GummyBear || thisEnemyType == enemyType.GummyRat)
        {
            particlesManager.Particels(1, transform.position);
        }
    }
    */
}
