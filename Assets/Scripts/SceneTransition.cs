using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Transform transitionPoint; // Specific point in the next scene where the player will appear
    public bool itemCollected = false;

    private void OnTriggerEnter2D(Collider2D other)

    {
        Debug.Log("Egg value: " + itemCollected); // Check the value returned by GetEgg()
        if (other.CompareTag("Player"))
        {
            // Check if the player is allowed to transition to the next scene based on the egg value
            if (itemCollected)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                if (transitionPoint != null)
                {
                    other.transform.position = transitionPoint.position;
                }
            }
            else
            {
                Debug.Log("Egg is false. Player cannot transition to the next scene.");
            }
        }
    }
}

