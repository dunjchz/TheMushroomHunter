using UnityEngine;

public class MushroomPuzzle : MonoBehaviour
{
    // Reference to the UI Panel
    public GameObject panel;

    void Start()
    {
        // Ensure the panel is hidden at the start
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    // Called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Show the panel
            if (panel != null)
            {
                panel.SetActive(true);
            }
        }
    }

    // Called when another collider exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Hide the panel
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }
}

