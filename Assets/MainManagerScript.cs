using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManagerScript : MonoBehaviour
{
    public static MainManagerScript Instance;

    [SerializeField] GameObject box;
    [SerializeField] GameObject player;

    [SerializeField] float transitionDuration;
    private BoxScript boxScript;

    [Header("DamagePanel")]
    [SerializeField] GameObject DamagePanel;
    private DamagePanelScript damagePanelScript;
    [SerializeField] public Vector2 panel_position;
    [SerializeField] public Vector2 panel_size;

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
        DamagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TransformToDamagePanel(transitionDuration);
        } 

        if(Input.GetKeyDown(KeyCode.Mouse0) && DamagePanel.activeSelf)
        {
            Debug.Log("Multiplier: " + damagePanelScript.GetMultiplier());
        }
    }

    public void TransformToDamagePanel(float timeDuration)
    {
        player.SetActive(false);
        boxScript.Resize(panel_position, panel_size);
        DamagePanel.SetActive(true);
    }
}
