using UnityEditor.Overlays;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public enum PickUptypes
    {
        Armor,
    };
    [SerializeField] PickUptypes ThisPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (ThisPickUp)
            {
                case PickUptypes.Armor:
                    Debug.Log("Armor Aquired");
                    //Health_Player.Health(1)
                    Destroy(gameObject);
                    break;

                default:
                    Debug.LogError("Invalid Pickup type");
                    break;
            }

        }
    }
}
