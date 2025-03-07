using UnityEngine;

public class BouncemellowDestructionScript : MonoBehaviour
{
    private bool playerDetected = false;
    public bool destroyBounceMellow;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            Debug.Log("Player Detected!" + playerDetected);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
      {
       if (collision.gameObject.CompareTag("Player"))
       {
         playerDetected = false;
         Debug.Log("Player Left platform!" + playerDetected);
            if( destroyBounceMellow == true)
            {
                Destroy(gameObject);
            }
       }
    }
}
