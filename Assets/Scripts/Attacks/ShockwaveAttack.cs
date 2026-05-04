using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveAttack : IAttack
{
    PurinAttacksScript purin;
    int amount;
    float delay;

    public ShockwaveAttack(PurinAttacksScript purin, int amount, float delay)
    {
        this.purin = purin;
        this.amount = amount;
        this.delay = delay;
    }

    public IEnumerator Execute()
    {
        yield return purin.SchockwaveAttack(amount, delay);
    }
}
