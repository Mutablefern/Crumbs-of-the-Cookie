using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    [SerializeField] float bossSpeed = 1;
    [SerializeField] List<Transform> positions;
    [SerializeField] Animator animator;
    [SerializeField] GameObject wallBreakObject;
    [SerializeField] GameObject collapseHolder;
    [SerializeField] Transform camera_transform;
    [SerializeField] Transform cameraTarget;
    BreakingWallTrigger wallBreakTrigger;
    Vector3 direction;
    bool arrived;
    int currentTarget = 0;
    float targetDistance;
    float range = 1;
    int totalTargets;
    bool cameraMove = false;
    Vector3 cameraOffset;

    void Start()
    {
        totalTargets = positions.Count;
        StartCoroutine(attackPattern());
        wallBreakTrigger = wallBreakObject.GetComponent<BreakingWallTrigger>();
        cameraOffset = (camera_transform.position - gameObject.transform.position);
    }

    private void Update()
    {
        if (cameraMove)
        {
            camera_transform.position = Vector3.Lerp(camera_transform.position, cameraTarget.position + cameraOffset, Time.deltaTime * 2);
        }
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
        yield return new WaitForSeconds(10f);
        yield return new WaitForSeconds(10f);
        yield return new WaitForSeconds(10f);
        yield return new WaitForSeconds(3f);
        SlamAttack();
        yield return new WaitForSeconds(2f);
        wallBreakTrigger.TriggerBreak();
        yield return new WaitForSeconds(5f);
        yield return new WaitForSeconds(10f);
        cameraMove = true;
        yield return new WaitForSeconds(5f);
        collapseHolder.SetActive(false);
    }

    private void SlamAttack()
    {
        animator.SetTrigger("Slam");
        //SLAM SOUND HERE
    }

    


}
