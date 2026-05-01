using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurinAttacksScript : MonoBehaviour
{
    [Header("First Attack")]
    [SerializeField] public GameObject foodPrefab;
    [SerializeField] private List<Vector3> foodSpawnPoints;

    [Header("Second Attack")]
    [SerializeField] public GameObject shockwavePrefab;

    private ShockwaveScript shockwaveScript;

    [SerializeField] float speed;
    [SerializeField] float thickness;
    [SerializeField] float maxRadius;

    private float HoleStart = 0.2f;
    private float HoleEnd = 1f;
    // Start is called before the first frame update
    void Start()
    {
        shockwaveScript = shockwavePrefab.GetComponent<ShockwaveScript>();
        
        shockwaveScript.speed = speed;
        shockwaveScript.thickness = thickness;
        shockwaveScript.maxRadius = maxRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(FoodAttack(4));
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Vector3 spawnPoint = new Vector3(transform.position.x,transform.position.y-2,0);
            Instantiate(shockwavePrefab, spawnPoint, Quaternion.Euler(0,0,180));
        }
    }

    public IEnumerator FoodAttack(int numberOfAttacks)
    {
        float yOffset = -2f;
        for(int i=0; i<numberOfAttacks; i++)
        {
            if(i%2 == 0)
            {
                foreach (Vector3 spawnPoint in foodSpawnPoints)
                {
                    Instantiate(foodPrefab, spawnPoint, Quaternion.identity);
                }
                yield return new WaitForSeconds(1f);
            }
            else
            {
                foreach (Vector3 spawnPoint in foodSpawnPoints)
                {
                    Vector3 newSpawnPoint = new Vector3(spawnPoint.x, spawnPoint.y *-1f + yOffset, spawnPoint.z);
                    Instantiate(foodPrefab, newSpawnPoint, Quaternion.identity);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public void EditAllSpawnPoint(List<Vector3> newSpawnPoints)
    {
        foodSpawnPoints = newSpawnPoints;
    }

    public void AddSpawnPoint(List<Vector3> newSpawnPoint)
    {
        foodSpawnPoints.AddRange(newSpawnPoint);
    }

    public IEnumerator SchockwaveAttack()
    {
        // wersja z segmentami:
        // int segments = 8;
        // float radius = 3f;

        // for (int i = 0; i < segments; i++)
        // {
        //     float angle = Mathf.Lerp(0, -Mathf.PI, (float)i / (segments - 1));

        //     Vector2 pos = new Vector2(
        //         Mathf.Cos(angle),
        //         Mathf.Sin(angle)
        //     ) * radius;
        //     Instantiate(segmentPrefab, pos, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90));
        // }
        // yield return new WaitForSeconds(0.1f);
        yield return null;
    }
}
