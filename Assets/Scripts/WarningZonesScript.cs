using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningZonesScript : MonoBehaviour
{
    [SerializeField] public GameObject warningZonePrefab;
    public bool isWarningZoneActive = false;
    

    public void ShowWarningZone(Vector2 position, Vector2 size)
    {
        StartCoroutine(ShowWarningZoneCoroutine(position, size, 1f));
    }

    IEnumerator ShowWarningZoneCoroutine(Vector2 position, Vector2 size, float duration)
    {
        isWarningZoneActive = true;
        GameObject warningZone = Instantiate(warningZonePrefab, position, Quaternion.identity);
        warningZone.GetComponent<SpriteRenderer>().size = size;
        yield return new WaitForSeconds(duration);
        Destroy(warningZone);
        isWarningZoneActive = false;
    }
}
