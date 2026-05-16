using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
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
    [SerializeField] float holeLength;
    [SerializeField] float time;
    [SerializeField] int numberOfAttacks;

    private float HoleStart = 0.2f;
    private float HoleEnd = 1f;

    [Header("Third Attack")]
    [SerializeField] public GameObject StraightFoodPrefab;
    [SerializeField] Vector2 startPoint;


    private Stats stats;

    void Awake()
    {
        stats = GetComponent<HealthScript>().stats;
    }
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
            StartCoroutine(SchockwaveAttack(numberOfAttacks, time));
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(StraightFoodAttack());
        }
    }

    public IEnumerator FoodAttack(int numberOfAttacks)
    {
        foodPrefab.GetComponent<FoodScript>().attack = stats.attack / 4;
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

    public IEnumerator SchockwaveAttack(int numberOfAttacks, float timeBetweenAttacks )
    {
        for(int i=0; i<numberOfAttacks; i++)
        {
            Vector3 spawnPoint = new Vector3(transform.position.x,transform.position.y-2,0);
            GeneratePositions();
            HoleStart = holePositions[i];
            HoleEnd = HoleStart + holeLength;
            shockwaveScript.mat.SetFloat("_HoleStart", HoleStart);
            shockwaveScript.mat.SetFloat("_HoleEnd", HoleEnd);
            shockwaveScript.attack = stats.attack;
            Instantiate(shockwavePrefab, spawnPoint, Quaternion.Euler(0,0,180));
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    List<float> holePositions = new List<float>();

    void GeneratePositions() //shuffle random
    {
        holePositions.Clear();
        
        int count = numberOfAttacks;
        float start = 0.5f;
        float step = 0.1f;

        for (int i = 0; i < count; i++)
        {
            holePositions.Add(start + i * step);
        }

        // shuffle
        for (int i = 0; i < holePositions.Count; i++)
        {
            float temp = holePositions[i];
            int randomIndex = Random.Range(i, holePositions.Count);
            holePositions[i] = holePositions[randomIndex];
            holePositions[randomIndex] = temp;
        }
    }

    IEnumerator StraightFoodAttack()
    {
        StartCoroutine(SpawnPairStraightFood(new Vector2(0, 1f), Quaternion.Euler(0,0,180)));
        StartCoroutine(SpawnPairStraightFood(new Vector2(0, -1f), Quaternion.Euler(0,0,0)));
        yield return null;
    }

    IEnumerator SpawnPairStraightFood(Vector2 offsetFromStartPoint,Quaternion rotation)
    {
        for(int i=0; i<3; i++)
        {
            //starting position moving towards the middle of the square 3 times
            for(int j=0; j<3; j++)
            {
                Instantiate(StraightFoodPrefab, startPoint + offsetFromStartPoint, rotation);
                Instantiate(StraightFoodPrefab, (startPoint + offsetFromStartPoint)*-1f, rotation);
                yield return new WaitForSeconds(0.5f);
            }
            startPoint = new Vector2(startPoint.x - 1f, startPoint.y);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
