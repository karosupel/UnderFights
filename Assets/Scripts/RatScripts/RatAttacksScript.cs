using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttacksScript : MonoBehaviour
{
    WarningZonesScript warningZonesScript;

    [Header("Spinning Attack")]
    [SerializeField] public GameObject SpiningGameObject; //virus will have a toxic effect on the player, making them take damage over time (player turning green)
    [SerializeField] float spinningSpeed;
    [SerializeField] int spins;

    [SerializeField] float angleWhenColumnSpawns;
    private bool coroutineRunning = false;

    [Header("Virus Attack")]

    [SerializeField] public GameObject virusPrefab;
    [SerializeField] List<float> virusSpawnX;
    [SerializeField] float virusSpawnY;
    [SerializeField] int virusWaves;

    [SerializeField] float virusWaveFrequency;
    [SerializeField] float virusWaveAmplitude;
    [SerializeField] float virusFallSpeed;
    [SerializeField] float virusLifetime;

    [Header("Column Attack")]

    [SerializeField] float columnIndex;
    [SerializeField] int columnWaves;

    [Header("Cheese Attack")]
    [SerializeField] GameObject cheesePrefab;
    [SerializeField] float speed;
    [SerializeField] float lifetime;
    [SerializeField] float timeOffset;
    [SerializeField] float height;
    [SerializeField] List<Vector2> cheeseSpawnPositions;

    
    void Start()
    {
        warningZonesScript = FindObjectOfType<WarningZonesScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SpinningAttack(spinningSpeed, spins, angleWhenColumnSpawns));
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(VirusSinAttack(virusWaves));
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(VirusColumnAttack(columnIndex, columnWaves));
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(CheeseAttack(speed, lifetime));
        }
    }

    IEnumerator SpinCoroutine(float speed, int spins, float angleWhenColumnSpawns)
    {
        coroutineRunning = true;
        Vector2 spawn_location = MainManagerScript.Instance.box.transform.position;
        GameObject spinningObject =
            Instantiate(SpiningGameObject, spawn_location, Quaternion.identity);

        float rotated = 0f;
        float targetRotation = spins * 360f;

        if(speed > 0)
        {
            while (rotated < targetRotation)
            {
                float step = speed * Time.deltaTime;

                spinningObject.transform.Rotate(0, 0, step);

                rotated += step;

                if(rotated%angleWhenColumnSpawns >= 0 && rotated%angleWhenColumnSpawns < step && rotated <= targetRotation)
                {
                    StartCoroutine(VirusColumnAttack(columnIndex, columnWaves));
                    columnIndex *= -1;
                }

                yield return null; // czekaj do następnej klatki
            }
        }
        else if (speed < 0)
        {
            targetRotation = -targetRotation;
             while (rotated > targetRotation)
            {
                float step = speed * Time.deltaTime;

                spinningObject.transform.Rotate(0, 0, step);

                rotated += step;

                if(rotated%angleWhenColumnSpawns <= 0 && rotated%angleWhenColumnSpawns > step && rotated >= targetRotation)
                {
                    StartCoroutine(VirusColumnAttack(columnIndex, columnWaves));
                    columnIndex *= -1;
                }

                yield return null; // czekaj do następnej klatki
            }
        }
        coroutineRunning = false;
        Destroy(spinningObject);
    }

    void ChangeSpinDirection()
    {
        spinningSpeed = -spinningSpeed;
    }

    public IEnumerator SpinningAttack(float spinningSpeed, int spins, float angleWhenColumnSpawns)
    {
        warningZonesScript.ShowWarningZone(MainManagerScript.Instance.box.transform.position, new Vector2(3f, 1f));
        warningZonesScript.ShowWarningZone(MainManagerScript.Instance.box.transform.position, new Vector2(1f, 3f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpinCoroutine(spinningSpeed, spins, angleWhenColumnSpawns));
        yield return new WaitUntil(() => !coroutineRunning);
    }

    void SpawnVirus(float x, float waveFrequency = 2f, float waveAmplitude = 2.1f, float fallSpeed = 2f, float lifetime = 5f)
    {
        GameObject virus = Instantiate(virusPrefab, new Vector3(x, virusSpawnY, 0), Quaternion.identity);
        VirusScript virusScript = virus.GetComponent<VirusScript>();
        virusScript.waveFrequency = waveFrequency;
        virusScript.waveAmplitude = waveAmplitude;
        virusScript.fallSpeed = fallSpeed;
        virusScript.lifetime = lifetime;
    }

    IEnumerator SpawnVirusWave(int currentHole, float waveFrequency = 2f, float waveAmplitude = 2.1f, float fallSpeed = 2f, float lifetime = 5f)
    {
        foreach(float x in virusSpawnX)
        {
            if (x != currentHole)
            {
                SpawnVirus(x, waveFrequency, waveAmplitude, fallSpeed, lifetime);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator VirusSinAttack(int waves, float waveFrequency = 2f, float waveAmplitude = 2.1f, float fallSpeed = 2f, float lifetime = 5f)
    {
        MainManagerScript.Instance.boxScript.Resize(MainManagerScript.Instance.f_panel_position, new Vector2(0.5f, 0.7f));
        int currentHole = Random.Range(0, virusSpawnX.Count);
        for (int i = 0; i < waves; i++)
        {
            StartCoroutine(SpawnVirusWave(currentHole, waveFrequency, waveAmplitude, fallSpeed, lifetime));
            yield return new WaitForSeconds(1f);
            currentHole += 1;
            if (currentHole >= virusSpawnX.Count)
            {
                currentHole = 0;
            }
        }
        yield return new WaitForSeconds(lifetime-1.6f);
    }

    IEnumerator VirusColumnAttack(float columnIndex, int waves)
    {
        warningZonesScript.ShowWarningZone(new Vector2(columnIndex, -1), new Vector2(0.6f, 5f));
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < waves; i++)
        {
            float offset = Random.Range(-0.3f, 0.3f);
            SpawnVirus(columnIndex, waveFrequency: 0f, waveAmplitude: 0f, fallSpeed: 4f, lifetime: 3f);
            yield return new WaitForSeconds(0.05f);
            SpawnVirus(columnIndex+offset, waveFrequency: 0f, waveAmplitude: 0f, fallSpeed: 4f, lifetime: 3f);
            yield return new WaitForSeconds(0.05f);
            SpawnVirus(columnIndex+offset, waveFrequency: 0f, waveAmplitude: 0f, fallSpeed: 4f, lifetime: 3f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator CheeseAttack(float speed, float lifetime)
    {
        MainManagerScript.Instance.boxScript.Resize(MainManagerScript.Instance.f_panel_position, new Vector2(0.52f, 0.71f));
        foreach (Vector2 position in cheeseSpawnPositions)
        {
            warningZonesScript.ShowWarningZone(position - new Vector2(0f,2f), new Vector2(0.5f, 2.5f));
            yield return new WaitForSeconds(1f);
            SpawnCheese(position, speed, lifetime: lifetime);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(lifetime-1.6f);
    }

    void SpawnCheese(Vector2 position, float speed, float lifetime, float timeOffset = 0f, float height = 4f)
    {
        GameObject cheese =Instantiate(cheesePrefab, position, Quaternion.identity);
        cheese.GetComponent<CheeseScript>().speed = speed;
        cheese.GetComponent<CheeseScript>().lifetime = lifetime;
        cheese.GetComponent<CheeseScript>().timeOffset = timeOffset;
        cheese.GetComponent<CheeseScript>().height = height;

    }
}
