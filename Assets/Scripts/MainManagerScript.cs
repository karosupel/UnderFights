using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainManagerScript : MonoBehaviour
{
    public static MainManagerScript Instance;

    [SerializeField] public GameObject box;
    [SerializeField] public GameObject player;

    TypingScript typingScript;

    public GameObject activeEnemy;

    [SerializeField] float transitionDuration;
    private BoxScript boxScript;

    [Header("DamagePanel")]
    [SerializeField] GameObject DamagePanel;
    private DamagePanelScript damagePanelScript;
    [SerializeField] public Vector2 panel_position;
    [SerializeField] public Vector2 panel_size;

    [Header("MainPanel")]
    [SerializeField] GameObject MainPanel;
    [SerializeField] TMPro.TMP_Text MainPanelText;

    [Header("FightPanel")]
    [SerializeField] public Vector2 f_panel_position;
    [SerializeField] public Vector2 f_panel_size;

    [Header("CheckPanel")]
    [SerializeField] GameObject CheckPanel;
    [SerializeField] TMPro.TMP_Text CheckPanelText;

    [Header("ItemPanel")]
    [SerializeField] GameObject ItemPanel;

    [Header("Events")]

    public UnityEvent OnFightStart;

    [Header("UI Events")]
    [SerializeField] Button fightButton;
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

    // Start is called before the first frame update
    void Start()
    {
        boxScript = box.GetComponent<BoxScript>();
        damagePanelScript = DamagePanel.GetComponent<DamagePanelScript>();
        typingScript = GetComponent<TypingScript>();
        typingScript.dialogueText = MainPanelText;
        activeEnemy = GameObject.FindGameObjectWithTag("Enemy");

        //setting the main panel active
        DamagePanel.SetActive(false);
        boxScript.Resize(panel_position, panel_size);
        player.SetActive(false);
        typingScript.StartTyping("* The air is filled with the smell of pudding");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TransformToFightPanel();
        } 

        if(Input.GetKeyDown(KeyCode.L))
        {
            TransformToMainPanel("* Suddenly you became very hungry...");
        } 
    }

    public void TransformToDamagePanel()
    {
        MainPanel.SetActive(false);
        player.SetActive(false);
        boxScript.Resize(panel_position, panel_size);
        ItemPanel.SetActive(false);
        DamagePanel.SetActive(true);
    }

    public void TransformToMainPanel(string Text)
    {
        StartCoroutine(TransformToMainPanelCorouting(Text));
    }

    public IEnumerator TransformToMainPanelCorouting(string Text)
    {
        DamagePanel.SetActive(false);
        ItemPanel.SetActive(false);
        player.SetActive(false);

        boxScript.SmoothResize(panel_position, panel_size, transitionDuration);
        yield return new WaitForSeconds(transitionDuration);

        MainPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(fightButton.gameObject);
        typingScript.dialogueText = MainPanelText;
        typingScript.StartTyping(Text);
    }

    public void TransformToFightPanel()
    {
        player.SetActive(true);
        boxScript.SmoothResize(f_panel_position, f_panel_size, transitionDuration);
        DamagePanel.SetActive(false);
        ItemPanel.SetActive(false);
        MainPanel.SetActive(false);
        OnFightStart?.Invoke();
    }

    public void TransformToCheckPanel()
    {
        boxScript.Resize(panel_position, panel_size);
        ItemPanel.SetActive(false);
        MainPanel.SetActive(true);
        string text = "* You check the enemy's stats... \n\n" +
                      "* HP: " + activeEnemy.GetComponent<HealthScript>().stats.health + "     " +
                      "* ATK: " + activeEnemy.GetComponent<HealthScript>().stats.attack + "     " +
                      "* DEF: " + activeEnemy.GetComponent<HealthScript>().stats.defense;
        typingScript.StartTyping(text);
    }

    public void TransformToItemPanel()
    {
        boxScript.Resize(panel_position, panel_size);
        MainPanel.SetActive(false);
        ItemPanel.SetActive(true);
    }
}
