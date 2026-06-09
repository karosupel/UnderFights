using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatFightScript : MonoBehaviour
{
    RatAttacksScript ratAttacksScript;
    HealthScript healthScript;
    public int attackIndex;

    [Header("Spinning Attack Settings")]
    [SerializeField] float spinningSpeed;
    [SerializeField] int spins;
    [SerializeField] float angleWhenColumnSpawns;

    [Header("Virus Sin Attack Settings")]
    [SerializeField] int virusWaves;
    [SerializeField] float virusWaveFrequency;

    [Header("Cheese Attack Settings")]
    [SerializeField] float cheeseSpeed;
    [SerializeField] float cheeseLifetime;

    List<IAttack> attacks = new List<IAttack>();

    void Awake()
    {
        ratAttacksScript = GetComponent<RatAttacksScript>();
        healthScript = GetComponent<HealthScript>();
        attackIndex = 0;

        attacks.Add(new SpinningAttack(ratAttacksScript, spinningSpeed, spins, angleWhenColumnSpawns));
        attacks.Add(new VirusSinAttack(ratAttacksScript, virusWaves, virusWaveFrequency));
        attacks.Add(new CheeseAttack(ratAttacksScript, cheeseSpeed, cheeseLifetime));
    }

    public void StartFight()
    {
        //CheckPhase();
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        if (attackIndex >= attacks.Count)
        {
            attackIndex = UnityEngine.Random.Range(0, attacks.Count);
        }

        yield return attacks[attackIndex].Execute();

        yield return new WaitForSeconds(1.2f);

        attackIndex++;

        yield return new WaitForSeconds(0.6f);

        MainManagerScript.Instance.TransformToMainPanel("* The toxins are rushing through your body...");

    }

    void CheckPhase()
    {

        if (healthScript.health <= 0.25f * healthScript.maxHealth)
        {
            
        }
        else if (healthScript.health <= 0.5f * healthScript.maxHealth)
        {
            
        }
        else if(healthScript.health <= 0.75f * healthScript.maxHealth)
        {
            
        }
        
    }
}
