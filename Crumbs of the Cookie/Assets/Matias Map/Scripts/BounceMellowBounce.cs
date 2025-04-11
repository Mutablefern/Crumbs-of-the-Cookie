using UnityEngine;

public class BounceMellowBounce : MonoBehaviour
{
    [SerializeField] float Bounciness = 20f;
    [SerializeField] float timeInAir = 1f;
    MovementScript movementScript;
    


    private void Awake()
    {
        movementScript = GetComponent<MovementScript>();
    }

    public void Update()
    {
        BouncinessIncrease();
    }

    public void BouncinessIncrease()
    {
        if(movementScript.IsGrounded() == false)
        {
            timeInAir = timeInAir * Time.deltaTime;

        }

        Bounciness += timeInAir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Bounciness, ForceMode2D.Impulse);
            timeInAir = 0f;
        }
    }
}
