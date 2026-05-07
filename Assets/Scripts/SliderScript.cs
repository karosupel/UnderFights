using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;

    [SerializeField] float moveSpeed;

    private Vector3 nextPosition;
    
    void Start()
    {
        transform.position = pointA;
        nextPosition = pointB;
    }

    void OnEnable()
    {
        transform.position = pointA;
        nextPosition = pointB;
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA) ? pointB : pointA;
        }
    }
}
