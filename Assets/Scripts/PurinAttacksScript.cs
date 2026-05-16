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
    [SerializeField] public GameObject StraightFoodPrefab;
    [SerializeField] Vector2 startPoint;
    [SerializeField] float offsetFromStartPoint;

    [SerializeField] public int amount_of_food;
    [SerializeField] public float offset_between_food;

    [SerializeField] public int number_of_repetitions;

    [SerializeField] public float time_between_repetitions;

    [SerializeField] public float time_between_food_spawn;
    bool ThirdAttackFinished = false;


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
            StartCoroutine(StraightFoodAttack(amount_of_food, offset_between_food, number_of_repetitions, time_between_repetitions, time_between_food_spawn));
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
            int randomIndex = UnityEngine.Random.Range(i, holePositions.Count);
            holePositions[i] = holePositions[randomIndex];
            holePositions[randomIndex] = temp;
        }
    }

    public IEnumerator StraightFoodAttack(int amount_of_food, float offset_between_food, int number_of_repetitions, float time_between_repetitions, float time_between_food_spawn)
    {
        ThirdAttackFinished = false;
        amount_of_food = this.amount_of_food;
        offset_between_food = this.offset_between_food;
        number_of_repetitions = this.number_of_repetitions;
        time_between_repetitions = this.time_between_repetitions;
        time_between_food_spawn = this.time_between_food_spawn;
        StartCoroutine(SpawnHorizontalPairStraightFood(new Vector2(0, offsetFromStartPoint), Quaternion.Euler(0,0,180)));
        StartCoroutine(SpawnHorizontalPairStraightFood(new Vector2(0, -offsetFromStartPoint), Quaternion.Euler(0,0,0)));
        StartCoroutine(SpawnVerticalPairStraightFood(new Vector2(-offsetFromStartPoint, 0), Quaternion.Euler(0,0,270)));
        StartCoroutine(SpawnVerticalPairStraightFood(new Vector2(offsetFromStartPoint, 0), Quaternion.Euler(0,0,90)));
        yield return new WaitUntil(() => ThirdAttackFinished);
    }

    IEnumerator SpawnHorizontalPairStraightFood(Vector2 offsetFromStartPoint,Quaternion rotation)
    {
        float offset = Math.Abs(offsetFromStartPoint.y);
        Vector2 startPoint = this.startPoint;
        for(int i=0; i<number_of_repetitions; i++)
        {
            //starting position moving towards the middle of the square 3 times
            for(int j=0; j<amount_of_food; j++)
            {
                Instantiate(StraightFoodPrefab, startPoint + offsetFromStartPoint + new Vector2(offset, 0), rotation);
                Instantiate(StraightFoodPrefab, startPoint + offsetFromStartPoint - new Vector2(offset, 0), rotation);
                yield return new WaitForSeconds(time_between_food_spawn);
            }
            offset -= offset_between_food;
            if(Math.Abs(offset) > offset_between_food)
            {
                offset = 0f;
            }
            yield return new WaitForSeconds(time_between_repetitions);
        }
    }

    IEnumerator SpawnVerticalPairStraightFood(Vector2 offsetFromStartPoint,Quaternion rotation)
    {
        float offset = Math.Abs(offsetFromStartPoint.x);
        Vector2 startPoint = this.startPoint;
        for(int i=0; i<number_of_repetitions; i++)
        {
            //starting position moving towards the middle of the square 3 times
            for(int j=0; j<amount_of_food; j++)
            {
                Instantiate(StraightFoodPrefab, startPoint + offsetFromStartPoint + new Vector2(0, offset), rotation);
                Instantiate(StraightFoodPrefab, startPoint + offsetFromStartPoint - new Vector2(0, offset), rotation);
                yield return new WaitForSeconds(time_between_food_spawn);
            }
            offset -= offset_between_food;
            if(Math.Abs(offset) > offset_between_food)
            {
                offset = 0;
            }
            yield return new WaitForSeconds(time_between_repetitions);
        }
        ThirdAttackFinished = true;
    }
}
