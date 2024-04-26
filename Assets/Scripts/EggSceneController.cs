using UnityEngine;
using UnityEngine.UI;

public class EggSceneController : MonoBehaviour
{
    public float interactionRange = 2f;

    public GameObject lockPanel;

    // Start is called before the first frame update
    void Start()
    {
        lockPanel.SetActive(false);
    }

    public void ActivatePanel()
    {
        Debug.Log("Panel Activated!");
        lockPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePanel()
    {
        Debug.Log("Panel closed");
        Time.timeScale = 1f; // Resume the game
        lockPanel.SetActive(false);
    }

}
