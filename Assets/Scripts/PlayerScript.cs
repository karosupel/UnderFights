using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;

    private HealthScript healthScript;


    void Start()
    {
        healthScript = GetComponent<HealthScript>();
    }

    void OnEnable()
    {
        transform.position = MainManagerScript.Instance.box.transform.position;
    }

    void Update()
    {
        
    }

    //movement:
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horizontal, vertical).normalized;
        rb.velocity = move * speed;
    }

}
