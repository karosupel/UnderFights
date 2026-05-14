using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPanelScript : MonoBehaviour
{
    public List<Button> ActiveItems;
    // Start is called before the first frame update
    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(ActiveItems[FindNotNullIndex(ActiveItems)].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int FindNotNullIndex(List<Button> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i] != null)
            {
                EventSystem.current.SetSelectedGameObject(list[i].gameObject);
                return i;
            }
        }
        return -1;
    }
}
