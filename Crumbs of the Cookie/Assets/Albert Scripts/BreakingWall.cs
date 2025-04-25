using System.Collections;
using UnityEngine;

public class BreakingWall : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb_box;
    [SerializeField] bool trigger;
    [SerializeField] float breakPower = 1;

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
            Break();
            StartCoroutine(timer());
        }
    }

    public void BreakWall()
    {
        trigger = true;
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(0.1f);
        trigger = false;
    }

    IEnumerator timer2()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.layer = 8;
    }

    private void Break()
    {
        rb_box.mass = 10;
        rb_box.AddForce(new Vector2(5,-1f) * breakPower, ForceMode2D.Impulse);
        StartCoroutine (timer2());
    }

}