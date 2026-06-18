using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Cinemachine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] public float maxHealth;
    //to delete this later v
    public float health;
    public Slider slider;
    public GameObject sliderCanvas;
    public TextMeshProUGUI hpValueText;

    public Animator? animator;

    public bool animationFinished = false;

    [SerializeField] public Stats stats;

    CinemachineImpulseSource impulseSource;

    public bool isPoisoned = false;

    public UnityEvent OnDeath;
    
    void Start()
    {
        maxHealth = stats.health;
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if(animator != null)
        {
            animator = GetComponent<Animator>();
        }
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
        if(health <= 0)
        {
            health = 0;
            OnDeath?.Invoke();
            if(gameObject.tag == "Player")
            {
                //help me
            }
            else
            {
                sliderCanvas.SetActive(true);
                hpValueText.text = health.ToString();
                SmoothSliderUpdate(health);
            }
        }
        else if(gameObject.tag=="Player")
        {
            hpValueText.text = health.ToString() + "/" + maxHealth.ToString();
            slider.value = health;
            CinemashineManager.Instance.CameraShake(impulseSource);
            gameObject.GetComponent<ParticleSystem>().Play();
        }
        else if (gameObject.tag!="Player")
        {
            animator.SetBool("isDamaged",true);
            sliderCanvas.SetActive(true);
            hpValueText.text = health.ToString();
            SmoothSliderUpdate(health);
            StartCoroutine(LetAnimationFinish(1f));
        }
    }

    public void PlayDeathAnimation()
    {
        StartCoroutine(DeathAnimation(1.8f));
    }

    IEnumerator DeathAnimation(float time)
    {
        animator.SetBool("isDead",true);
        yield return new WaitForSeconds(time);
        MainManagerScript.Instance.YouveWon();
    }

    IEnumerator LetAnimationFinish(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("isDamaged",false);
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;

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
        yield return new WaitForSeconds(0.5f);
        sliderCanvas.SetActive(false);
    }

    public void PoisonPlayer(float damagePerSecond, float duration)
    {
        isPoisoned = true;
        StartCoroutine(PoisonCoroutine(damagePerSecond, duration));
    }

    IEnumerator PoisonCoroutine(float damagePerSecond, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            TakeDamage(damagePerSecond);
            elapsedTime += 1;
            yield return new WaitForSeconds(1f);
        }
        isPoisoned = false;
    }
}
