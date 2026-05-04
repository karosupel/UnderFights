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

    void OnEnable()
    {
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        yield return attacks[attackIndex].Execute();

        attackIndex++;

        if (attackIndex >= attacks.Count)
        {
            attackIndex = UnityEngine.Random.Range(0, attacks.Count);
        }
    }

    //TODO: Add phases depending on boss health
    //current phase i tak dalej
}
