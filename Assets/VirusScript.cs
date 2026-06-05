using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusScript : MonoBehaviour
{
    public float fallSpeed = 3f;
    public float waveAmplitude = 1f;
    public float waveFrequency = 2f;

    public float lifetime = 5f;

    private float startX;
    private float spawnTime;

    void Start()
    {
        startX = transform.position.x;
        spawnTime = Time.time;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        float t = Time.time - spawnTime;

        float x = startX + Mathf.Sin(t * waveFrequency) * waveAmplitude;
        float y = transform.position.y - fallSpeed * Time.deltaTime;

        transform.position = new Vector3(x, y, 0);
    }
}
