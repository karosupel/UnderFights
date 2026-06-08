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

    public float damage = 10f;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthScript>().TakeDamage(damage);
        }
    }
}
