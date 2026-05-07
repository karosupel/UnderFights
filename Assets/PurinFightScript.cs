using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PurinFightScript : MonoBehaviour
{
    PurinAttacksScript purinAttacks;
    public int attackIndex;

    [Header("1st attack Settings")]
    [SerializeField] int numberOfFoofAttacks;

    [Header("2nd attack Settings")]
    [SerializeField] int numberOfShockwaveAttacks;
    [SerializeField] float timeBetweenAttacks;

    List<IAttack> attacks = new List<IAttack>();

    void Awake()
    {
        purinAttacks = GetComponent<PurinAttacksScript>();

        attacks.Add(new FoodAttack(purinAttacks, numberOfFoofAttacks));
        attacks.Add(new ShockwaveAttack(purinAttacks, numberOfShockwaveAttacks, timeBetweenAttacks));
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

        attackIndex++;

        yield return new WaitForSeconds(0.6f);

        StartCoroutine(MainManagerScript.Instance.TransformToMainPanel("* You still have some chocolate left on your clothes..."));

    }

    //TODO: Add phases depending on boss health
    //current phase i tak dalej
}
