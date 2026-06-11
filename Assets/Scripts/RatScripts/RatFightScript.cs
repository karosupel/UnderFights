using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatFightScript : MonoBehaviour
{
    RatAttacksScript ratAttacksScript;
    public HealthScript healthScript;
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
        CheckPhase();
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
            Debug.Log("Phase 4");
            attacks[0] = new SpinningAttack(ratAttacksScript, spinningSpeed + 0.6f, spins+2, 90f);
            attacks[1] = new VirusSinAttack(ratAttacksScript, virusWaves+2, virusWaveFrequency+0.4f);
            attacks[2] = new CheeseAttack(ratAttacksScript, cheeseSpeed-1f, cheeseLifetime+3f);
        }
        else if (healthScript.health <= 0.5f * healthScript.maxHealth)
        {
            Debug.Log("Phase 3");
            attacks[0] = new SpinningAttack(ratAttacksScript, spinningSpeed + 0.4f, spins+2, 90f);
            attacks[1] = new VirusSinAttack(ratAttacksScript, virusWaves+2, virusWaveFrequency+0.3f);
            attacks[2] = new CheeseAttack(ratAttacksScript, cheeseSpeed-0.75f, cheeseLifetime+2f);
        }
        else if(healthScript.health <= 0.75f * healthScript.maxHealth)
        {
            Debug.Log("Phase 2");
            attacks[0] = new SpinningAttack(ratAttacksScript, spinningSpeed + 0.2f, spins+1, 120f);
            attacks[1] = new VirusSinAttack(ratAttacksScript, virusWaves+1, virusWaveFrequency+0.2f);
            attacks[2] = new CheeseAttack(ratAttacksScript, cheeseSpeed-0.5f, cheeseLifetime+1f);
        }
        
    }
}
