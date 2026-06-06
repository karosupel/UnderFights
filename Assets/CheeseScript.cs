using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    [SerializeField] AnimationCurve cheeseCurve;
    float elapsedTime = 0f;
    float t; // Duration of the movement in seconds
    Vector2 startPosition;
    public float speed = 2f;

    public float lifetime = 5f; 

    public float timeOffset;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime / speed;
        t = timeOffset + elapsedTime;

        transform.position = Vector3.Lerp(
            startPosition,
            startPosition + Vector2.down * 2f, // Replace with your actual end position
            cheeseCurve.Evaluate(t)
        );

        if(t >= 1f)
        {
            t = 0f;
            elapsedTime = 0f;
        }
    }
}
