using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    [SerializeField] GameObject TorchHitbox;
    [SerializeField] float attackRange;
    [SerializeField] float attackDuration;

    bool isAttacking;

    private void Update()
    {
        
    }
    void OnLightAttack(InputValue inputValue)
    {
        if (inputValue.isPressed && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        GameObject Hitbox = Instantiate(TorchHitbox, transform.position + new Vector3(attackRange, 0, 0), Quaternion.identity, transform);
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        Destroy(Hitbox);
    }

}

