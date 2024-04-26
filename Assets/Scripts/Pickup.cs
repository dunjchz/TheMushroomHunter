using System.Collections;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public CanvasRenderer panel;
    public TMP_Text textDisplay;
    public string pickupText;

    private bool isTyping = false;
    public SceneTransition sceneTransition;

    private bool activeDialog = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && activeDialog)
        {
            ContinueDialog();
        }
    }

    public void TriggerPickup()
    {
        Time.timeScale = 0f;
        panel.gameObject.SetActive(true);
        activeDialog = true;
        StartCoroutine(Typing());
    }

    public void ContinueDialog()
    {
        if (isTyping) return;
        panel.gameObject.SetActive(false);
        activeDialog = false;
        Time.timeScale = 1f;
        if (sceneTransition) sceneTransition.itemCollected = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator Typing()
    {
        isTyping = true;
        textDisplay.SetText("");
        foreach (char letter in pickupText.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        isTyping = false;
    }
}
