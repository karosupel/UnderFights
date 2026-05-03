using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PurinFightScript : MonoBehaviour
{
    PurinAttacksScript purinAttacks;
    public int attackIndex;
    public enum attacks //musze zrobic tablice/slownik mozliwych atakow do wykonania
    {
        FoodAttack,
        ShockwaveAttack
    }

    void Awake()
    {
        purinAttacks = GetComponent<PurinAttacksScript>();
    }

    void OnEnable()
    {
        //tu bedzie wywoływana funkcja attack
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if(attackIndex < 2) //tutaj zamiast 2 bedzie ilosc mozliwych atakow
        {
            // wykonuje sie attack pod attacks[attackIndex];
        }
        else if(attackIndex >= 2)
        {
            //get random index for the next attack
        }
        //kiedy zakonczy sie walka attackIndex++;
    }
}
