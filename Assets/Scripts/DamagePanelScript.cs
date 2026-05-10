using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DamagePanelScript : MonoBehaviour
{
    public GameObject slider;

    [SerializeField] private float perfectOffset = 0.5f;
    [SerializeField] private float missThreshold = 0.9f;

    private SliderScript sliderScript;
    private Canvas damagePanelCanvas;
    private Vector2 panel_position;
    private Vector2 panel_size;

    private GameObject activeEnemy;
    private HealthScript activeEnemyHealthScript;

    private GameObject activeEnemyCanvas;

    private int damage;
    
    void Awake()
    {
        slider = transform.GetChild(0).gameObject;
        damagePanelCanvas = gameObject.GetComponentInChildren<Canvas>();
        sliderScript = slider.GetComponent<SliderScript>();

        activeEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if(activeEnemy != null)
        {
            activeEnemyHealthScript = activeEnemy.GetComponent<HealthScript>();
            activeEnemyCanvas = activeEnemy.transform.GetChild(0).gameObject;
        }

    }
    void Start()
    {
        panel_position = MainManagerScript.Instance.panel_position;
        panel_size = MainManagerScript.Instance.panel_size;

        sliderScript.pointA = panel_position - new Vector2(panel_size.x/2*7,0);
        sliderScript.pointB = panel_position + new Vector2(panel_size.x/2*7,0);

        //setting everything to match panel size:
        slider.transform.position = sliderScript.pointA;
        slider.transform.localScale = new Vector3(0.2f, panel_size.y * 7f, 0f); 
        slider.SetActive(true);

        RectTransform rt = damagePanelCanvas.GetComponent<RectTransform>();
        rt.sizeDelta = panel_size * 7f;
    }
    public float GetMultiplier()
    {
        float distance = Mathf.Abs(slider.transform.position.x - panel_position.x);
        float maxDistance = panel_size.x * 7f / 2f;

        float normalized = distance / maxDistance;

        //perfect
        if (distance <= perfectOffset)
            return 1f;

        //miss
        if (normalized >= missThreshold)
            return 0f;

        //normal hit 
        float multiplier = 1f - normalized;
        return multiplier;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && gameObject.activeSelf)
        {
            Debug.Log("Multiplier: " + GetMultiplier());
            float player_attack = MainManagerScript.Instance.player.GetComponent<HealthScript>().stats.attack;
            damage = Mathf.RoundToInt(GetMultiplier() * player_attack); // * enemy def later
            activeEnemyHealthScript.TakeDamage(damage);
            //play canvas animation here
            StartCoroutine(ShowHealthBar()); //placeholder
        }
    }

    IEnumerator ShowHealthBar()
    {
        activeEnemyCanvas.SetActive(true);
        slider.GetComponent<SliderScript>().enabled = false; //stop the slider movement
        yield return new WaitUntil(() => activeEnemyHealthScript.animationFinished); 
        yield return new WaitForSeconds(0.5f);
        slider.GetComponent<SliderScript>().enabled = true; //resume the slider movement
        MainManagerScript.Instance.TransformToFightPanel();
    }
}
