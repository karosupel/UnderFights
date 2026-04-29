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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            healthScript.TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }

}
