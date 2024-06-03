using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InputHandler : MonoBehaviour
{
    // Reference to the Input Field
    public TMP_InputField inputField;

    // Reference to the Button
    public Button submitButton;

    // The correct answer
    private string correctAnswer = "chanterelles";

    void Start()
    {
        // Ensure the panel is active at the start
        gameObject.SetActive(true);

        // Add a listener to the button to call the CheckInput function when clicked
        submitButton.onClick.AddListener(CheckInput);
    }

    // Function to check the player's input
    void CheckInput()
    {
        // Get the text from the input field
        string playerInput = inputField.text;

        // Check if the input matches the correct answer
        if (playerInput.Equals(correctAnswer, System.StringComparison.OrdinalIgnoreCase))
        {
            // If the input is correct, proceed to the next scene
            LoadNextScene();
        }
        else
        {
            // If the input is incorrect, display an error message (optional)
            Debug.Log("Incorrect answer. Please try again.");
        }
    }

    // Function to load the next scene
    void LoadNextScene()
    {
        // Replace "NextSceneName" with the actual name of the next scene
        SceneManager.LoadScene("Ending scene");
    }
}
