using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BigFoodScript : MonoBehaviour
{
    [SerializeField] GameObject miniFoodPrefab;
    public int amountOfMiniFood;
    public float lifetime;

    public Vector2 velocityVector;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(velocityVector.x, velocityVector.y, 0) * speed * Time.deltaTime;
    }

    public IEnumerator Explosion()
    {
        yield return new WaitForSeconds(lifetime);
        for (int i = 0; i < amountOfMiniFood; i++)
        {
            float angle = i * (360f / amountOfMiniFood);
            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            Instantiate(miniFoodPrefab, spawnPosition, Quaternion.Euler(0, 0, angle));
        }
        Destroy(gameObject);
    }
}
