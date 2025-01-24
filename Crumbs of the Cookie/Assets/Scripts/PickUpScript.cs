using UnityEditor.Overlays;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public enum PickUptypes
    {
        Armor,
    };
    [SerializeField] PickUptypes ThisPickUp;

    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (ThisPickUp)
            {
                case PickUptypes.Armor:
                    Debug.Log("Armor Aquired");
                    //Player.GetComponent<Health_Player>().Health(1)
                    Destroy(gameObject);
                    break;

                default:
                    Debug.LogError("Pickup type " + ThisPickUp + " doesnt have a function");
                    break;
            }

        }
    }
}
