using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogObject : MonoBehaviour
{
    public CanvasRenderer panel;
    public TMP_Text textDisplay;
    public string[] dialogTextLines;
    private int index = 0;

    private bool isTyping = false;

    private bool activeDialog = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetDialog();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && activeDialog)
        {
            ContinueDialog();
        }
    }

    public void StartDialog()
    {
        ResetDialog();
        Time.timeScale = 0f;
        panel.gameObject.SetActive(true);
        activeDialog = true;
        StartCoroutine(Typing());
    }

    public void ContinueDialog()
    {
        if (isTyping) return;
        index += 1;

        if (index < dialogTextLines.Length)
        {
            StartCoroutine(Typing());
        }
        else
        {
            ResetDialog();
        }
    }

    IEnumerator Typing()
    {
        isTyping = true;
        textDisplay.SetText("");
        foreach (char letter in dialogTextLines[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        isTyping = false;
    }

    private void ResetDialog()
    {
        Debug.Log(string.Format("Resetting dialog"));
        panel.gameObject.SetActive(false);
        index = 0;
        textDisplay.SetText("");
        Time.timeScale = 1f;
        activeDialog = false;
    }
}
