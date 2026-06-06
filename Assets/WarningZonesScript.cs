using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningZonesScript : MonoBehaviour
{
    [SerializeField] public GameObject warningZonePrefab;
    

    public void ShowWarningZone(Vector2 position, Vector2 size)
    {
        GameObject warningZone = Instantiate(warningZonePrefab, position, Quaternion.identity);
        warningZone.GetComponent<SpriteRenderer>().size = size;
        StartCoroutine(CloseWarningZone(warningZone, duration: 1f));
    }

    IEnumerator CloseWarningZone(GameObject warningZone, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(warningZone);
    }
}
