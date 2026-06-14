using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] List<string> DialogueLines;
    public bool continuePressed;
    public Canvas MainCanvas;
    public GameObject StraightFoodPrefab;
    TypingScript typingScript;

    // Start is called before the first frame update
    void Start()
    {
        MainCanvas.GetComponent<CanvasGroup>().interactable = false;
        typingScript = GetComponent<TypingScript>();
        StartCoroutine(TutorialCoroutine());
    }

    public IEnumerator TutorialCoroutine()
    {
        for(int i=0; i<7; i++)
        {
            typingScript.StartTyping(DialogueLines[i]);
            yield return new WaitUntil(() => continuePressed);
            continuePressed = false;
        }
        MainCanvas.GetComponent<CanvasGroup>().interactable = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            continuePressed = true;
        }
    }
}
