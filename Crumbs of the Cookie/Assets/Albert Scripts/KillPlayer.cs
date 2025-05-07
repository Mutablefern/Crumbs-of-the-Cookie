using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] Health_Player health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health.Health(1);
        }
    }


}
