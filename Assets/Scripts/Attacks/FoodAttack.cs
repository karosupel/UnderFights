using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAttack : IAttack
{
    PurinAttacksScript purin;
    int amount;

    public FoodAttack(PurinAttacksScript purin, int amount)
    {
        this.purin = purin;
        this.amount = amount;
    }

    public IEnumerator Execute()
    {
        yield return purin.FoodAttack(amount);
    }
}
