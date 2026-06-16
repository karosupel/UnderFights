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
    public GameObject foodPrefab;
    [SerializeField] private List<Vector3> foodSpawnPoints;

    [SerializeField] GameObject FightPanel;
    TypingScript typingScript;
    private Stats stats;

    private bool isFirstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<HealthScript>().stats;
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

    public IEnumerator FoodAttack(int numberOfAttacks)
    {
        yield return new WaitUntil(() => isFirstTime == false);
        foodPrefab.GetComponent<FoodScript>().attack = stats.attack / 4;
        float yOffset = -2f;
        for(int i=0; i<numberOfAttacks; i++)
        {
            if(i%2 == 0)
            {
                foreach (Vector3 spawnPoint in foodSpawnPoints)
                {
                    Instantiate(foodPrefab, spawnPoint, Quaternion.identity);
                }
                yield return new WaitForSeconds(1f);
            }
            else
            {
                foreach (Vector3 spawnPoint in foodSpawnPoints)
                {
                    Vector3 newSpawnPoint = new Vector3(spawnPoint.x, spawnPoint.y *-1f + yOffset, spawnPoint.z);
                    Instantiate(foodPrefab, newSpawnPoint, Quaternion.identity);
                }
                yield return new WaitForSeconds(1f);
            }
        }
        typingScript.StartTyping("");
        yield return new WaitForSeconds(1.5f);
        MainManagerScript.Instance.TransformToMainPanel(DialogueLines[11]);
    }

    public IEnumerator Talking()
    {
        for(int i=7; i<11; i++)
        {
            typingScript.StartTyping(DialogueLines[i]);
            yield return new WaitUntil(() => continuePressed);
            continuePressed = false;
        }
        isFirstTime = false;
    }

    public void StartFight()
    {
        if(isFirstTime == true)
        {
            StartCoroutine(Talking());
            StartCoroutine(FoodAttack(4));
        }
        else
        {
            StartCoroutine(FoodAttack(4));
        }
    }
}
