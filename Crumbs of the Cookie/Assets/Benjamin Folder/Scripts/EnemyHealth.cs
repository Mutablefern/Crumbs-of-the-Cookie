using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public enum enemyType
    {
        GummyBear,
        GummyRat,
        Butterroll,
    };
    [Tooltip("choose what type of enemy this is")]
    [SerializeField] enemyType thisEnemyType;
    [Tooltip("3 or less for rat, more for bear")]
    [SerializeField] int health;
    ParticlesManager particlesManager;
    EnemyPointMovement enemyPointMovement;
    EnemyLedgeChecking enemyLedgeChecking;
    EnemyLedgeCheckingRollyPolly enemyLedgeCheckRollyPolly;
    Rigidbody2D rb_Enemy;
    Vector3 direction;
    public Vector3 sourceOfKnockback;
    



    private void Start()
    {
        rb_Enemy = GetComponent<Rigidbody2D>();
        particlesManager = GameObject.Find("Particle Manager").GetComponent<ParticlesManager>();
        enemyPointMovement = GetComponent<EnemyPointMovement>();
        enemyLedgeChecking = GetComponent<EnemyLedgeChecking>();
        enemyLedgeCheckRollyPolly = GetComponent<EnemyLedgeCheckingRollyPolly>();
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
            StartCoroutine(KnockBackPoint());
            
            
        }
        if (thisEnemyType == enemyType.GummyRat)
        {
            particlesManager.Particels(1, transform.position);
            StartCoroutine(KnockBackLedge());
        }
        if(thisEnemyType == enemyType.Butterroll)
        {
            particlesManager.Particels(0, transform.position);
            StartCoroutine(KnockBackLedgeRollyPolly());
        }
    }
    IEnumerator KnockBackPoint()
    {
        enemyPointMovement.enemyState = 2;
        enemyPointMovement.Knockback();
        yield return new WaitForSeconds(1);
        enemyPointMovement.enemyState = 3;
    }

    IEnumerator KnockBackLedge()
    {
        enemyLedgeChecking.enemyState = 2;
        enemyLedgeChecking.Knockback();
        yield return new WaitForSeconds(0.5f);
        enemyLedgeChecking.enemyState = 1;
    }

    IEnumerator KnockBackLedgeRollyPolly()
    {
        enemyLedgeCheckRollyPolly.enemyState = 2;
        enemyLedgeCheckRollyPolly.Knockback();
        yield return new WaitForSeconds(0.75f);
        enemyLedgeCheckRollyPolly.enemyState = 1;
    }
}
