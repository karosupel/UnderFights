using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPanelScript : MonoBehaviour
{
    public List<Button> ActiveItems;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(ActiveItems[0].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
