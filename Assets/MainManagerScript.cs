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
    private GameObject slider;
    private SliderScript sliderScript;
    private Canvas damagePanelCanvas;
    [SerializeField] Vector2 panel_position;
    [SerializeField] Vector2 panel_size;

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
        slider = DamagePanel.transform.GetChild(0).gameObject;
        sliderScript = slider.GetComponent<SliderScript>();
        damagePanelCanvas = DamagePanel.GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TransformToDamagePanel(transitionDuration);
        } 
    }

    public void TransformToDamagePanel(float timeDuration)
    {
        player.SetActive(false);
        boxScript.Resize(panel_position, panel_size);
        RectTransform rt = damagePanelCanvas.GetComponent<RectTransform>();
        rt.sizeDelta = panel_size * 7f;
        
        // boxScript.SmoothResize(panel_position, panel_size, timeDuration); //resizing box to destination value
        // yield return new WaitForSeconds(timeDuration);

        //resizing and activating the slider:
        slider.transform.localScale = new Vector3(0.2f, panel_size.y * 7f, 0f); 
        slider.SetActive(true); 

        //setting final points to slider:
        sliderScript.pointA = panel_position - new Vector2(panel_size.x/2*7,0);
        sliderScript.pointB = panel_position + new Vector2(panel_size.x/2*7,0);

        DamagePanel.SetActive(true);
    }
}
