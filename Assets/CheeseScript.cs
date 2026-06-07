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

    public float height = 4f;

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

        float t = Mathf.Repeat(elapsedTime + timeOffset, 1f);

        transform.position = Vector3.Lerp(
            startPosition,
            startPosition + Vector2.down * height,
            cheeseCurve.Evaluate(t)
        );
    }
}
