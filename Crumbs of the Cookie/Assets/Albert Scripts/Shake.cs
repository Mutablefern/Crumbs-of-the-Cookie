using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(shake());
    }

    IEnumerator shake()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = gameObject.transform.position + new Vector3(0.005f, 0.005f, 0);
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = gameObject.transform.position - new Vector3(0.005f, 0.005f, 0);
        StartCoroutine(shake());
    }
    
}
