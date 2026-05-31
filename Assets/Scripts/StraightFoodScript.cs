using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightFoodScript : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] float speed;
    [SerializeField] float damage;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    void Update()
    {
        transform.position = transform.position + transform.up * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
