using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : IAttack
{
    PurinAttacksScript purin;
    int amoutOfBigFood;
    int amountOfMiniFood;

    public ExplosionAttack(PurinAttacksScript purin, int amoutOfBigFood, int amountOfMiniFood)
    {
        this.purin = purin;
        this.amoutOfBigFood = amoutOfBigFood;
        this.amountOfMiniFood = amountOfMiniFood;
    }

    public IEnumerator Execute()
    {
        yield return purin.ExplosionAttack(amoutOfBigFood, amountOfMiniFood);
    }
}
