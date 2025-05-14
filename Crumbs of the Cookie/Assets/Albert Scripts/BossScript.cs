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
    [SerializeField] GameObject OutOfBounds1;
    [SerializeField] GameObject OutOfBounds2;
    [SerializeField] GameObject OutOfBounds3;
    [SerializeField] GameObject OutOfBounds4;
    [SerializeField] SceneManagement sceneManagement;
    BreakingWallTrigger wallBreakTrigger;
    AudioManager audioManager;
    Vector3 direction;
    bool arrived;
    int currentTarget = 0;
    float targetDistance;
    float range = 1;
    int totalTargets;
    bool cameraMove = false;
    Vector3 cameraOffset;
    bool moveActive = false;
    bool shake;
    void Start()
    {
        totalTargets = positions.Count;
        StartCoroutine(attackPattern());
        wallBreakTrigger = wallBreakObject.GetComponent<BreakingWallTrigger>();
        cameraOffset = (camera_transform.position - gameObject.transform.position);
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        if (currentTarget < positions.Count && moveActive)
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
    IEnumerator camShake()
    {
        camera_transform.position = camera_transform.position + new Vector3(0.2f, 0.2f, 0);
        yield return new WaitForSeconds(0.05f);
        camera_transform.position = camera_transform.position + new Vector3(-0.2f, 0.2f, 0);
        yield return new WaitForSeconds(0.05f);
        camera_transform.position = camera_transform.position + new Vector3(0.2f, -0.2f, 0);
        yield return new WaitForSeconds(0.05f);
        camera_transform.position = camera_transform.position + new Vector3(-0.2f, -0.2f, 0);
        if (shake)
        {
            StartCoroutine(camShake());
        }
    }

    IEnumerator attackPattern()
    {
        yield return new WaitForSeconds(5.5f);
        StartCoroutine(camShake());
        audioManager.playSFX(audioManager.bossroar);
        shake = true;
        yield return new WaitForSeconds(2f);
        shake = false;
        yield return new WaitForSeconds(0.5f);
        moveActive = true;
        yield return new WaitForSeconds(10f);
        yield return new WaitForSeconds(10f);
        yield return new WaitForSeconds(10f);
        yield return new WaitForSeconds(3f);
        SlamAttack();
        yield return new WaitForSeconds(2f);
        audioManager.playSFX(audioManager.bossslam);
        wallBreakTrigger.TriggerBreak();
        yield return new WaitForSeconds(5f);
        yield return new WaitForSeconds(10f);
        cameraMove = true;
        camera_transform.transform.parent = null;
        OutOfBounds1.SetActive(false);
        OutOfBounds2.SetActive(false);
        OutOfBounds3.SetActive(false);
        OutOfBounds4.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(camShake());
        shake = true;
        yield return new WaitForSeconds(0.5f);
        collapseHolder.SetActive(false);
        yield return new WaitForSeconds(4f);
        shake = false;
        animator.SetTrigger("Crush");
        yield return new WaitForSeconds(4f);
        cameraTarget.position = new Vector3(transform.position.x-5, 0, transform.position.y);
        yield return new WaitForSeconds(5f);
        sceneManagement.ChangeScene("Main Menu Scene");
    }

    private void SlamAttack()
    {
        animator.SetTrigger("Slam");
    }
}
