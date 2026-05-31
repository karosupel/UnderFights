using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] GameObject bigFoodPrefab;
    [SerializeField] float xMaxSpawn;
    [SerializeField] float xMinSpawn;
    [SerializeField] float ySpawn;

    [SerializeField] int amoutOfBigFood;
    [SerializeField] int amountOfMiniFood;
    [SerializeField] float lifetime;

    [SerializeField] Vector2 velocityVector;
    [SerializeField] float bigFoodspeed;


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
            StartCoroutine(ExplosionAttack(amoutOfBigFood, amountOfMiniFood));
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

    List<float> holePositions = new List<float>();
    public IEnumerator SchockwaveAttack(int numberOfAttacks, float timeBetweenAttacks )
    {
        MainManagerScript.Instance.boxScript.Resize(MainManagerScript.Instance.f_panel_position, new Vector2(1.2f, 0.7f));
        for(int i=0; i<numberOfAttacks; i++)
        {
            Vector3 spawnPoint = new Vector3(transform.position.x,transform.position.y-2,0);
            GeneratePositions(holePositions, 0.5f, 0.1f);
            HoleStart = holePositions[i];
            HoleEnd = HoleStart + holeLength;
            shockwaveScript.mat.SetFloat("_HoleStart", HoleStart);
            shockwaveScript.mat.SetFloat("_HoleEnd", HoleEnd);
            shockwaveScript.attack = stats.attack;
            Instantiate(shockwavePrefab, spawnPoint, Quaternion.Euler(0,0,180));
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    void GeneratePositions(List<float> positions, float start, float step) //shuffle random
    {
        positions.Clear();
        
        int count = numberOfAttacks;

        for (int i = 0; i < count; i++)
        {
            positions.Add(start + i * step);
        }

        // shuffle
        for (int i = 0; i < positions.Count; i++)
        {
            float temp = positions[i];
            int randomIndex = UnityEngine.Random.Range(i, positions.Count);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }
    }

    public IEnumerator ExplosionAttack(int amoutOfBigFood, int amountOfMiniFood)
    {
        List<float> spawnPositionsX = new List<float>();
        GeneratePositions(spawnPositionsX, xMinSpawn, 0.5f);

        for(int i = 0; i < amoutOfBigFood; i++)
        {
            BigFoodScript bigFoodScript = bigFoodPrefab.GetComponent<BigFoodScript>();
            bigFoodScript.amountOfMiniFood = amountOfMiniFood;
            /*bigFoodScript.lifetime = lifetime;
            bigFoodScript.velocityVector = velocityVector;
            bigFoodScript.speed = bigFoodspeed;*/
            Instantiate(bigFoodPrefab, new Vector3(spawnPositionsX[i], ySpawn, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(lifetime);
    }

    
}
