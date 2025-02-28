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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LightHit"))
        {
            health--;

        }
        else if (collision.gameObject.CompareTag("HeavyHit"))
        {
            health=- 2;
        }
        if (health <= 0)
        {
            Debug.Log("Play Enemy death anim");
            Destroy(gameObject);
        }
    }
}
