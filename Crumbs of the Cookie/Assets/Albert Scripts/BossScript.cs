using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    [SerializeField] float bossSpeed = 1;
    [SerializeField] List<Transform> positions;
    Vector3 direction;
    bool arrived;
    int currentTarget = 0;
    float targetDistance;
    float range = 1;
    int totalTargets;

    void Start()
    {
        totalTargets = positions.Count;
        StartCoroutine(attackPattern());
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (currentTarget < positions.Count)
        {
            Movement(positions[currentTarget]);
        }
    }

    private void Movement(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, bossSpeed);

        if (range > Vector3.Distance(transform.position, target.position))
        {
            currentTarget += 1;
        }
    }

    IEnumerator attackPattern()
    {
        yield return new WaitForSeconds(5f);
    }

    private void SlamAttack()
    {

    }

    


}
