using UnityEngine;

public class BounceMellowBounce : MonoBehaviour
{
    [SerializeField] float Bounciness = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Bounciness, ForceMode2D.Impulse);
        }
    }
}
