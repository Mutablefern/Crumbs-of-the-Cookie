using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public void AttackEnd()
    {
        Debug.Log("No More");
        GetComponentInParent<AttackScript>().isAttacking = false;
    }
}
