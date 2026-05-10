using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class HealthScript : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] public float maxHealth;
    //to delete this later v
    public float health;
    public Slider slider;
    public TextMeshProUGUI hpValueText;

    public bool animationFinished = false;

    [SerializeField] public Stats stats;
    
    void Start()
    {
        maxHealth = stats.health;
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
        if(gameObject.tag=="Player")
        {
            hpValueText.text = health.ToString() + "/" + maxHealth.ToString();
            slider.value = health;
        }
        else
        {
            hpValueText.text = health.ToString();
            SmoothSliderUpdate(health);
        }
    }

    public void SmoothSliderUpdate(float newHealth)
    {
        StartCoroutine(SmoothSliderTransition(newHealth));
    }

    IEnumerator SmoothSliderTransition(float newHealth)
    {
        float elapsedTime = 0f;
        float duration = 1f; // Adjust the duration as needed
        float startingHealth = slider.value;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(startingHealth, newHealth, elapsedTime / duration);
            yield return null;
        }

        slider.value = newHealth; // Ensure it ends at the exact value
        animationFinished = true;
    }
}
