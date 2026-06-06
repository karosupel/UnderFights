using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttacksScript : MonoBehaviour
{
    WarningZonesScript warningZonesScript;

    [Header("First Attack")]
    [SerializeField] public GameObject SpiningGameObject; //virus will have a toxic effect on the player, making them take damage over time (player turning green)
    [SerializeField] float spinningSpeed;
    [SerializeField] int spins;
    private bool coroutineRunning = false;

    [Header("Second Attack")]

    [SerializeField] public GameObject virusPrefab;
    [SerializeField] List<float> virusSpawnX;
    [SerializeField] float virusSpawnY;
    [SerializeField] int virusWaves;

    [Header("Column Attack")]

    [SerializeField] float columnIndex;
    [SerializeField] int columnWaves;

    
    void Start()
    {
        warningZonesScript = FindObjectOfType<WarningZonesScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SpinningAttack());
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(VirusSinAttack(virusWaves));
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(VirusColumnAttack(columnIndex, columnWaves));
        }
    }

    IEnumerator SpinCoroutine(float speed, int spins)
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

    IEnumerator SpinningAttack()
    {
        warningZonesScript.ShowWarningZone(MainManagerScript.Instance.box.transform.position, new Vector2(3f, 1f));
        warningZonesScript.ShowWarningZone(MainManagerScript.Instance.box.transform.position, new Vector2(1f, 3f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpinCoroutine(spinningSpeed, 1));
        yield return new WaitUntil(() => !coroutineRunning);
        ChangeSpinDirection();
        StartCoroutine(SpinCoroutine(spinningSpeed, 1));
    }

    void SpawnVirus(float x, float waveFrequency = 2f, float waveAmplitude = 2.1f, float fallSpeed = 2f)
    {
        GameObject virus = Instantiate(virusPrefab, new Vector3(x, virusSpawnY, 0), Quaternion.identity);
        VirusScript virusScript = virus.GetComponent<VirusScript>();
        virusScript.waveFrequency = waveFrequency;
        virusScript.waveAmplitude = waveAmplitude;
        virusScript.fallSpeed = fallSpeed;
    }

    IEnumerator SpawnVirusWave(int currentHole)
    {
        foreach(float x in virusSpawnX)
        {
            if (x != currentHole)
            {
                SpawnVirus(x);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator VirusSinAttack(int waves)
    {
        MainManagerScript.Instance.boxScript.Resize(MainManagerScript.Instance.f_panel_position, new Vector2(0.5f, 0.7f));
        int currentHole = Random.Range(0, virusSpawnX.Count);
        for (int i = 0; i < waves; i++)
        {
            StartCoroutine(SpawnVirusWave(currentHole));
            yield return new WaitForSeconds(1f);
            currentHole += 1;
            if (currentHole >= virusSpawnX.Count)
            {
                currentHole = 0;
            }
        }
    }

    IEnumerator VirusColumnAttack(float columnIndex, int waves)
    {
        warningZonesScript.ShowWarningZone(new Vector2(columnIndex, -1), new Vector2(0.6f, 5f));
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < waves; i++)
        {
            float offset = Random.Range(-0.3f, 0.3f);
            SpawnVirus(columnIndex, waveFrequency: 0f, waveAmplitude: 0f, fallSpeed: 4f);
            yield return new WaitForSeconds(0.05f);
            SpawnVirus(columnIndex+offset, waveFrequency: 0f, waveAmplitude: 0f, fallSpeed: 4f);
            yield return new WaitForSeconds(0.05f);
            SpawnVirus(columnIndex+offset, waveFrequency: 0f, waveAmplitude: 0f, fallSpeed: 4f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
