using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public static MainMenuScript Instance;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject levelMenuCanvas;

    [SerializeField] Button PurinButton;
    [SerializeField] Button RatButton;

    public List<int> visitedScenes;

    BobScript bobScript;
    // Start is called before the first frame update
    void Start()
    {
        visitedScenes.Add(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToLevelMenu()
    {
        if(visitedScenes.Contains(2) == false)
        {
            PurinButton.interactable = false;
        }
        else
        {
            PurinButton.interactable = true;
        }
        if(visitedScenes.Contains(3) == false)
        {
            RatButton.interactable = false;
        }
        else
        {
            RatButton.interactable = true;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Debug.Log("bob");
            SceneManager.LoadScene(0);
        }
        else
        {
            levelMenuCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
        }
    }

    public void ToTutorial()
    {
        if(visitedScenes.Contains(1) == false)
        {
            visitedScenes.Add(1);
        }
        SceneManager.LoadScene(1);
    }

    public void ToPurin()
    {
        if(visitedScenes.Contains(2) == false)
        {
            visitedScenes.Add(2);
        }
        SceneManager.LoadScene(2);
    }

    public void ToRat()
    {
        if(visitedScenes.Contains(3) == false)
        {
            visitedScenes.Add(3);
        }
        SceneManager.LoadScene(3);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
