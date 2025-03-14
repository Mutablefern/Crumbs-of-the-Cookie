using System.Collections;
using UnityEngine;

public class BreakingWall : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb_box;
    [SerializeField] bool trigger;

    BoxCollider2D boxCollider;

    private void Start()
    {
        rb_box = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();    
    }

    private void Update()
    {
        if (trigger)
        {
            Break(transform);
            StartCoroutine(timer());
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(0.05f);
        trigger = false;
    }

    IEnumerator timer2()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.layer = 8;
    }

    public void Break(Transform force)
    {
        rb_box.mass = 10;
        rb_box.AddForce(new Vector2(5,-1f), ForceMode2D.Impulse);
        StartCoroutine (timer2());
    }

}
