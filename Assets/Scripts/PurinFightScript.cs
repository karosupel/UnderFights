using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PurinFightScript : MonoBehaviour
{
    PurinAttacksScript purinAttacks;

    HealthScript healthScript;
    public int attackIndex;

    [Header("1st attack Settings")]
    [SerializeField] int numberOfFoofAttacks;

    [Header("2nd attack Settings")]
    [SerializeField] int numberOfShockwaveAttacks;
    [SerializeField] float timeBetweenAttacks;

    [Header("3rd attack Settings")]
    [SerializeField] int amoutOfBigFood;
    [SerializeField] int amountOfMiniFood;

    List<IAttack> attacks = new List<IAttack>();

    void Awake()
    {
        purinAttacks = GetComponent<PurinAttacksScript>();
        healthScript = GetComponent<HealthScript>();

        attacks.Add(new FoodAttack(purinAttacks, numberOfFoofAttacks));
        attacks.Add(new ShockwaveAttack(purinAttacks, numberOfShockwaveAttacks, timeBetweenAttacks));
        attacks.Add(new ExplosionAttack(purinAttacks, amoutOfBigFood, amountOfMiniFood));
    }

    public void StartFight()
    {
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

        MainManagerScript.Instance.TransformToMainPanel("* You still have some chocolate left on your clothes...");

    }

    void Update()
    {
        if(healthScript.health < 0.6f * healthScript.maxHealth)
        {
            attacks[0] = new FoodAttack(purinAttacks, numberOfFoofAttacks + 2);
            attacks[1] = new ShockwaveAttack(purinAttacks, numberOfShockwaveAttacks + 2, timeBetweenAttacks - 0.2f);
            attacks[2] = new ExplosionAttack(purinAttacks, amoutOfBigFood + 2, amountOfMiniFood + 2);
            Debug.Log("Phase 2");
        }
    }

    //TODO: Add phases depending on boss health
    //current phase i tak dalej
}
