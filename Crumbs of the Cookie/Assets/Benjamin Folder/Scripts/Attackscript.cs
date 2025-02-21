using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    [SerializeField] GameObject TorchHitbox;
    [SerializeField] float attackRange;
    [SerializeField] float attackDuration;
    [SerializeField] int ammoAmmount;
    [SerializeField] int ammoMaxAmmount;
    [SerializeField] float reloadDuration;
    [SerializeField] GameObject torchFire;
    [SerializeField] Animator PlayerAni;

    bool isAttacking;

    void OnLightAttack(InputValue inputValue)
    {
        if (inputValue.isPressed && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            if (ammoAmmount > 0)
            {
                Debug.Log("attack");
                ammoAmmount--;
                TorchHitbox.SetActive(true);
                //start animation
                yield return new WaitForSeconds(attackDuration);
                TorchHitbox.SetActive(false);
                if (ammoAmmount <= 0)
                {
                    torchFire.SetActive(false);
                }
            }

            else
            {
                Debug.Log("reload");
                StartCoroutine(attackReloadCor());
            }
        }
    }

    IEnumerator attackReloadCor()
    {
        //start reload animation

        yield return new WaitForSeconds(reloadDuration);
        ammoAmmount = ammoMaxAmmount;
        torchFire.SetActive(true);

    }

}
