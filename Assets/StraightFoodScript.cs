using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightFoodScript : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] float speed;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    void Update()
    {
        transform.position = transform.position + transform.up * Time.deltaTime * speed;
    }
}
