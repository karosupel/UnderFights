using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManagerScript : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] GameObject player;

    [SerializeField] float transitionDuration;
    private BoxScript boxScript;

    [Header("DamagePanel")]
    [SerializeField] GameObject slider;
    private SliderScript sliderScript;
    [SerializeField] Vector2 panel_position;
    [SerializeField] Vector2 panel_size;

    // Start is called before the first frame update
    void Start()
    {
        boxScript = box.GetComponent<BoxScript>();
        sliderScript = slider.GetComponent<SliderScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(TransformToDamagePanel(transitionDuration));
        } 
    }

    public IEnumerator TransformToDamagePanel(float timeDuration)
    {
        player.SetActive(false);
        boxScript.SmoothResize(panel_position, panel_size, timeDuration); //resizing box to destination value
        yield return new WaitForSeconds(timeDuration);
        slider.transform.localScale = new Vector3(0.2f, panel_size.y * 7f, 0f); //resizing slider to fit the box
        slider.SetActive(true); //activating the slider
        //setting final points to slider:
        sliderScript.pointA = panel_position - new Vector2(panel_size.x/2*7,0);
        sliderScript.pointB = panel_position + new Vector2(panel_size.x/2*7,0);
    }
}
