using UnityEngine;

public class BounceMellowBounce : MonoBehaviour
{
    public float maxVelocity = 35f;
    private float bounceVelocity = 1f;
    private float Bounciness;
    public Rigidbody2D player_rb;
    public float guaranteedBounce = 20f;

    private void FixedUpdate()
    {
        if (player_rb.linearVelocityY > 45f)
        {
            player_rb.linearVelocity = Vector2.ClampMagnitude(player_rb.linearVelocity, maxVelocity);
        }
    }

    private void Update()
    {
        bounceVelocity = player_rb.linearVelocityY;

        Bounciness = bounceVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Mathf.Abs(Bounciness), ForceMode2D.Impulse);
            
        }

        if (collision.gameObject.CompareTag("Player"))
        {    
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * guaranteedBounce, ForceMode2D.Impulse);
        }
    }
}
