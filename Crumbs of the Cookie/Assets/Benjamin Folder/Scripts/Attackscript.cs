using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    [SerializeField] GameObject LightAttackHitbox;
    [SerializeField] GameObject HeavyAttackHitbox;
    [SerializeField] float LightAttackDuration;
    [SerializeField] float HeavyAttackStartLag;
    [SerializeField] int ammoAmmount;
    [SerializeField] int ammoMaxAmmount;
    [SerializeField] float reloadDuration;
    [SerializeField] GameObject torchFire;
    [SerializeField] Animator PlayerAnim;

    bool isReloading;
    public bool isAttacking;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnLightAttack(InputValue inputValue)
    {
        if (inputValue.isPressed && !isAttacking)
        {
            StartCoroutine(LightAttack());
        }
    }

    void OnHeavyAttack(InputValue inputValue)
    {
        if (inputValue.isPressed && !isAttacking)
        {
            StartCoroutine(HeavyAttack());
        }
    }

    IEnumerator LightAttack()
    {
        if (!isAttacking)
        {
           
            if (ammoAmmount > 0)
            {
                isAttacking = true;
                Debug.Log("Light attack");
                ammoAmmount--;
                LightAttackHitbox.SetActive(true);
                PlayerAnim.SetTrigger("Light attack");
                audioManager.playSFX(audioManager.lightattack);
                yield return new WaitForSeconds(LightAttackDuration);
                LightAttackHitbox.SetActive(false);
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
            isAttacking = false;
        }
    }
    IEnumerator HeavyAttack()
    {
        if (!isAttacking)
        {
            
            if (ammoAmmount > 0)
            {
                isAttacking = true;
                Debug.Log("Heavy attack");
                ammoAmmount--;
                PlayerAnim.SetTrigger("Heavy attack");
                yield return new WaitForSeconds(HeavyAttackStartLag * 0.8f);
                HeavyAttackHitbox.SetActive(true);
                audioManager.playSFX(audioManager.heavyattack);
                yield return new WaitForSeconds(0.3f);
                HeavyAttackHitbox.SetActive(false);
                if (ammoAmmount <= 0)
                {
                    torchFire.SetActive(false);
                }
            }

            else
            {
                if (!isReloading)
                {
                    Debug.Log("reload");
                    StartCoroutine(attackReloadCor());
                }
            }
        } 
    }

    IEnumerator attackReloadCor()
    {
        isReloading = true;
        audioManager.playSFX(audioManager.matchignite);
        PlayerAnim.SetTrigger("Ignite");
        yield return new WaitForSeconds(reloadDuration);
        ammoAmmount = ammoMaxAmmount;
        torchFire.SetActive(true);
        isReloading = false;
    }

}
