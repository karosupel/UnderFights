using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainPanelScript : MonoBehaviour
{
    
    public TMP_Text dialogueText;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;

    void Awake()
    {
        dialogueText = gameObject.GetComponentInChildren<TMP_Text>();
    }
    public void StartTyping(string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    
}
