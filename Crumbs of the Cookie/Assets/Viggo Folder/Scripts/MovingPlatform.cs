using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform pointA, pointB;
    public float moveSpeed;

    private Vector3 nextPosition;


    void Start()
    {
        nextPosition = pointB.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);


        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 contactNormal = collision.GetContact(0).normal;
            if (Mathf.Abs(contactNormal.y) > Mathf.Abs(contactNormal.x))
            {
                collision.gameObject.transform.parent = transform;
            }
        }
        else
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) collision.gameObject.transform.parent = null;
    }
}