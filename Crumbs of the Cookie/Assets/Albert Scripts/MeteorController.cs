using System.Collections;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [SerializeField] bool conatainsEnemy; //does it contain enemy?
    [SerializeField] Rigidbody2D rb_meteor; //link to rigidbody for setting Velocity
    [SerializeField] Transform target;
    [SerializeField] float speed = 1;

    Vector2 direction;

    void Start()
    {
        direction = new Vector2(target.position.x, target.position.y) - new Vector2(transform.position.x, transform.position.y); //the targets position relative to the meteors position
        
        var dir = target.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FixedUpdate()
    {
        rb_meteor.linearVelocity = direction.normalized * speed; //makes the meteor go to the target
        speed = speed * 1.002f;
    }

    public void changeTarget(Transform targetPosition)
    {
        target = targetPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && conatainsEnemy)
        {
            //spawn enemies
            speed = 0;
        }
        if (collision.gameObject.layer == 6 && !conatainsEnemy)
        {
            Debug.Log("kill");
            Destroy(gameObject);
        }
    }

}