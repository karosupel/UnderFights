using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{   
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;

    private HealthScript healthScript;
    private CinemachineImpulseSource impulseSource;


    void Start()
    {
        healthScript = GetComponent<HealthScript>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            impulseSource.enabled = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            impulseSource.enabled = true;
        }
    }

}
