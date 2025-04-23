using UnityEngine;

public class BounceMellowBounce : MonoBehaviour
{
    public float maxVelocity = 35f;
    private float bounceVelocity = 1f;
    private float bounceness;
    public Rigidbody2D player_rb;
    public float guaranteedBounce = 20f;
    public bool touchedBounceMellow;
    public float waitForTime;

    private void FixedUpdate()
    {
        if (player_rb.linearVelocityY > maxVelocity)
        {
            player_rb.linearVelocity = Vector2.ClampMagnitude(player_rb.linearVelocity, maxVelocity);
        }
    }

    private void Update()
    {
        bounceVelocity = player_rb.linearVelocityY;

        bounceness = bounceVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Mathf.Abs(bounceness), ForceMode2D.Impulse);
            touchedBounceMellow = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (touchedBounceMellow == true)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * guaranteedBounce, ForceMode2D.Impulse);
            }
        }
    }
}
