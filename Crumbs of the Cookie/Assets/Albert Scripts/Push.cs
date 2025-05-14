using System.Collections;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField] Rigidbody2D push_rb;
    int counter = 0;
    public void Start()
    {
        StartCoroutine(timer());
        push_rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(61);
        push_rb.AddForce(new Vector3(100, 0, 0));
        StartCoroutine(loop());
    }
    IEnumerator loop()
    {
        yield return new WaitForSeconds(0.1f);
        push_rb.AddForce(new Vector3(20, 0, 0));
        counter += 1;
        if (gameObject.transform.position.y >= 10)
        {
            StartCoroutine(loop());
        }
    }
}