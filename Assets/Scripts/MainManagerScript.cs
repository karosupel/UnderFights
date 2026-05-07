using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainManagerScript : MonoBehaviour
{
    public static MainManagerScript Instance;

    [SerializeField] public GameObject box;
    [SerializeField] public GameObject player;

    [SerializeField] float transitionDuration;
    private BoxScript boxScript;

    [Header("DamagePanel")]
    [SerializeField] GameObject DamagePanel;
    private DamagePanelScript damagePanelScript;
    [SerializeField] public Vector2 panel_position;
    [SerializeField] public Vector2 panel_size;

    [Header("MainPanel")]
    [SerializeField] GameObject MainPanel;
    private MainPanelScript mainPanelScript;

    [Header("FightPanel")]
    [SerializeField] public Vector2 f_panel_position;
    [SerializeField] public Vector2 f_panel_size;

    [Header("Events")]

    public UnityEvent OnFightStart;

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
        mainPanelScript = MainPanel.GetComponent<MainPanelScript>();

        //setting the main panel active
        DamagePanel.SetActive(false);
        boxScript.Resize(panel_position, panel_size);
        player.SetActive(false);
        mainPanelScript.StartTyping("* The air is filled with the smell of pudding");
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
            StartCoroutine(TransformToMainPanel("* Suddenly you became very hungry..."));
        } 

        if(Input.GetKeyDown(KeyCode.Mouse0) && DamagePanel.activeSelf)
        {
            Debug.Log("Multiplier: " + damagePanelScript.GetMultiplier());
            TransformToFightPanel();
            OnFightStart?.Invoke();
        }
    }

    public void TransformToDamagePanel()
    {
        MainPanel.SetActive(false);
        player.SetActive(false);
        boxScript.Resize(panel_position, panel_size);
        DamagePanel.SetActive(true);
    }

    public IEnumerator TransformToMainPanel(string Text)
    {
        DamagePanel.SetActive(false);
        player.SetActive(false);

        boxScript.SmoothResize(panel_position, panel_size, transitionDuration);
        yield return new WaitForSeconds(transitionDuration);

        MainPanel.SetActive(true);
        mainPanelScript.StartTyping(Text);
    }

    public void TransformToFightPanel()
    {
        player.SetActive(true);
        boxScript.SmoothResize(f_panel_position, f_panel_size, transitionDuration);
        DamagePanel.SetActive(false);
        MainPanel.SetActive(false);
    }
}
