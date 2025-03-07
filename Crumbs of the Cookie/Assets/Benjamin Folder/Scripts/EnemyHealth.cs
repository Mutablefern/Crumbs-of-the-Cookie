using System.Collections;
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
    EnemyPointMovement enemyPointMovement;
    Rigidbody2D rb_Enemy;
    Vector3 direction;
    public Vector3 sourceOfKnockback;
    int knockback = 2;
    int knockbackY = 1;




    private void Start()
    {
        rb_Enemy = GetComponent<Rigidbody2D>();
        particlesManager = GameObject.Find("Particle Manager").GetComponent<ParticlesManager>();
        enemyPointMovement = GetComponent<EnemyPointMovement>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LightHit"))
        {
           sourceOfKnockback = collision.transform.position;
           health--;
            DamageEffect();
        }
        else if (collision.gameObject.CompareTag("HeavyHit"))
        {
            sourceOfKnockback = collision.transform.position;
            health -= 2;
            DamageEffect();
        }
        if (health <= 0)
        {
            Debug.Log("Play Enemy death anim");
            Destroy(gameObject);
        }
    }
    public void DamageEffect()
    {
        if (thisEnemyType == enemyType.GummyBear)
        {
            particlesManager.Particels(1, transform.position);
            StartCoroutine(KnockBack());
            
            
        }
        if (thisEnemyType == enemyType.GummyRat)
        {
            particlesManager.Particels(1, transform.position);
        }
    }
    IEnumerator KnockBack()
    {
        enemyPointMovement.enemyState = 2;
        enemyPointMovement.Knockback();
        yield return new WaitForSeconds(1);
        enemyPointMovement.enemyState = 1;
    }
}
