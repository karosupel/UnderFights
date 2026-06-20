using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickAudio : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(AudioManagerScript.Instance != null)
        {
            AudioManagerScript.Instance.PlaySFX("click");
        }
    }
}
