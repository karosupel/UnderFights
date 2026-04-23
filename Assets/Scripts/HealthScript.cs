using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthScript : MonoBehaviour, IDamageable
{
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
        if(gameObject.tag=="Player")
        {
            hpValueText.text = health.ToString() + "/" + maxHealth.ToString();
        }
        else
        {
            hpValueText.text = health.ToString();
        }
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        slider.value = health;
        if(gameObject.tag=="Player")
        {
            hpValueText.text = health.ToString() + "/" + maxHealth.ToString();
        }
        else
        {
            hpValueText.text = health.ToString();
        }
    }
}
