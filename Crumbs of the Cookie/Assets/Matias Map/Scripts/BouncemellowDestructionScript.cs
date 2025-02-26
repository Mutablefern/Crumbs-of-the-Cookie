using UnityEngine;

public class BouncemellowDestructionScript : MonoBehaviour
{
    private bool PlayerDetected = false;


    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDetected = true;
            Debug.Log("Player Detected!" + PlayerDetected);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
      {
       if (collision.gameObject.CompareTag("Player"))
       {
         PlayerDetected = false;
         Debug.Log("Player Left platform!" + PlayerDetected);
       }
    }
}
