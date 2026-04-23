using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour, IDamageable
{   
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;

    [Header("Health")]
    [SerializeField] float maxHealth;
    //to delete this later v
    public float health;
    public Slider slider;
    public TextMeshProUGUI hpValueText;


    void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
        hpValueText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    //movement:
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horizontal, vertical).normalized;
        rb.velocity = move * speed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        slider.value = health;
        hpValueText.text = health.ToString() + "/" + maxHealth.ToString();
    }
}
